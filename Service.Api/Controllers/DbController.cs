using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Service.DAL.Models;

namespace Service.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DbController : ControllerBase
    {
        private readonly DAL.Repository.ISheet repo;
        private readonly Repository.IApiResponse resp;
        public DbController(DAL.Repository.ISheet _repo, Repository.IApiResponse _resp)
        {
            repo = _repo;
            resp = _resp;
        }

        //выводит все записи из таблиц Sheet1 и Sheet2
        [HttpGet]
        public async Task<Repository.IApiResponse> Get()
        {
            try
            {
                resp.AddData(await repo.GetAll());
            }
            catch (Exception ex)
            {
                resp.AddError(ex.Message);

            }
            return resp;
        }

        //загружает данные из файла Excel в таблицы Sheet1, Sheet2
        [HttpPost("upload/excel")]
        [Consumes("multipart/form-data")]
        public async Task<Repository.IApiResponse> UpdloadExcel(IFormFile file)
        {
            try
            {
                if (file != null)
                {
                    //проверим расширение файла
                    if (file.FileName.ToLower().Contains(".xls") || file.FileName.ToLower().Contains(".xlsx"))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            await file.CopyToAsync(ms);
                            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                            using (ExcelPackage package = new ExcelPackage(ms))
                            {
                                //проверяем колчисетво листов
                                if (package.Workbook.Worksheets.Count >= 2)
                                {
                                    Repository.IExcel excel = new Repository.Excel();
                                    
                                    //забираем данные из первого листа Excel
                                    var config1 = new MapperConfiguration(cfg => cfg.CreateMap<BaseSheet, Sheet1>());
                                    var mapper1 = new Mapper(config1);
                                    var sheet1 = mapper1.Map<Sheet1>(excel.LoadFromSheet(package, 0));
                                    //сохраняем полученные данные в таблицу Sheet1 БД
                                    if (await repo.Insert<DAL.Models.Sheet1>(sheet1) == null) resp.AddError("не удалось сохранить в БД данные листа 1");
                                    
                                    //забираем данные из второго листа Excel
                                    var config2 = new MapperConfiguration(cfg => cfg.CreateMap<BaseSheet, Sheet2>());
                                    var mapper2 = new Mapper(config2);
                                    var sheet2 = mapper2.Map<Sheet2>(excel.LoadFromSheet(package, 1));
                                    //сохраняем полученные данные в таблицу Sheet2 БД
                                    if (await repo.Insert<DAL.Models.Sheet2>(sheet2) == null) resp.AddError("не удалось сохранить в БД данные листа 2");
                                    
                                    //говорим, что все ОК, если нет ошибок
                                    if (!resp.HasErrors()) resp.AddData("Данные файла Excel успешно сохранены");
                                }
                                else
                                {
                                    //если количество листов меньше двух, выводим сообщение
                                    resp.AddError("количество листов должно быть не менне 2");
                                }
                            }
                        }
                    }
                    else
                    {
                        resp.AddError("загружаемый файл должен быть файлом Excel");
                    }
                }
                else
                {
                    resp.AddError("не верные параметры");
                }
            }
            catch (Exception ex)
            {
                resp.AddError(ex.Message);
            }
            return resp;
        }

        //получает наименование таблицы Sheet1 или Sheet2 в зависимости от значения идентификатора
        [HttpGet("sheet/name/{guid}")]
        public async Task<Repository.IApiResponse> GetTableNameById(string guid)
        {
            try
            {
                Guid Id = Guid.Parse(guid);
                resp.AddData(await repo.GetTableNameById(Id));
            }
            catch (Exception ex)
            {
                resp.AddError(ex.Message);
            }
            return resp;
        }

        [HttpGet("{guid}")]
        public async Task<Repository.IApiResponse> GetById(string guid)
        {
            try
            {
                Guid Id = Guid.Parse(guid);
                resp.AddData(await repo.GetById(Id));
            }
            catch (Exception ex)
            {
                resp.AddError(ex.Message);

            }
            return resp;
        }

        //удаляет запись и таблицы Sheet1 или Sheet2 по идентификатору
        //если запись удалена, возвращает true, иначе false
        [HttpDelete("{id}")]
        public async Task<Repository.IApiResponse> Delete(string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                resp.AddData(await repo.Delete(guid));
            }
            catch (Exception ex)
            {
                resp.AddError(ex.Message);
            }
            return resp;
        }

        //изменяет данные записи по id
        // PUT: api/Service.DAL/5
        [HttpPut("{id}")]
        public async Task<Repository.IApiResponse> Put(string id, DAL.Models.BaseSheet newSheet)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                //загружаем запись из БД по id
                DAL.Models.BaseSheet oldSheet = await repo.GetById(guid);
                if (oldSheet!=null)
                {
                    //копируем изменения на уровне сопоставления свойст классов
                    repo.TrackChanges(newSheet, oldSheet);
                    //сохраняем в бд измененную запись 
                    //(к сожжалению, в ORM Dapper не предусмотрено TrackChanges, поэтому будут сохранены все данные,
                    //а не только измененные в объекте)
                    resp.AddData(await repo.Update(oldSheet));
                }
                else
                {
                    //запись по Id в БД не найдена
                    resp.AddError(string.Format("запись с идентификатором {0} не найдена", id));
                }
            }
            catch (Exception ex)
            {
                resp.AddError(ex.Message);
            }
            return resp;
        }
    }
}
