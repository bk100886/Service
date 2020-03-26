using Service.DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace Service.DAL.Repository
{
    public interface ISheet
    {
        //возвращает созданные записи, их ID, время создания, упорядочивая по времени по возрастанию
        Task<List<BaseSheet>> GetAll();
        //возвращает запись по идентификатору Id, если возвращает null - запись не найдена
        Task<BaseSheet> GetById(Guid Id);
        //удаление записи по Id
        Task<bool> Delete(Guid Id);
        //вставляет запись в таблицу, возвращает Guid идентификатор вставленной записи
        Task<Guid> Insert<T>(T item);
        //обновить запись
        Task<bool> Update(BaseSheet item);
        //получает название таблицы по идентификатору Guid, 
        //если возвращает пустую строку, значит идентификатор не найден
        Task<string> GetTableNameById(Guid Id);
        //копирует из объекта FromObject в объект ToObject измененния свойств
        void TrackChanges(BaseSheet FromObject, BaseSheet ToObject);
    }
    public class Sheet : ISheet
    {
        string connectionString = null;
        public Sheet(string connection)
        {
            connectionString = connection;
        }

        public async Task<bool> Delete(Guid Id)
        {
            bool Result = false;
            string TableName = await GetTableNameById(Id);
            if (!string.IsNullOrEmpty(TableName))
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    switch (TableName)
                    {
                        case "Sheet1":
                            Result = (await connection.DeleteAsync<Sheet1>(Id)) > 0;
                            break;
                        case "Sheet2":
                            Result = (await connection.DeleteAsync<Sheet2>(Id)) > 0;
                            break;
                        default:
                            break;
                    }
                }
            }
            return Result;
        }

        public async Task<List<BaseSheet>> GetAll()
        {
            List<BaseSheet> Result = null;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"(SELECT * FROM Sheet1 s)
                                 UNION
                                 (SELECT * FROM Sheet2 s)
                                 ORDER BY s.Created";
                Result =  (await connection.QueryAsync<BaseSheet>(query)).ToList();
            }
            return Result;
        }

        public async Task<BaseSheet> GetById(Guid Id)
        {
            BaseSheet Result = null;
            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT *
                                FROM Sheet1 s
                                WHERE s.Id=@Id  
                                UNION
                                SELECT *
                                FROM Sheet2 s
                                WHERE s.Id=@Id";

                Result = (await connection.QueryAsync<Models.BaseSheet>(query, new { Id = Id })).FirstOrDefault();
            }

            return Result;
        }

        public async Task<string> GetTableNameById(Guid Id)
        {
            string Result = "";
            using (var connection = new SqlConnection(connectionString))
            {
                string query = @"SELECT ('Sheet1') AS TableName
                                FROM Sheet1 s
                                WHERE s.Id=@Id  
                                UNION
                                SELECT ('Sheet2') AS TableName
                                FROM Sheet2 s
                                WHERE s.Id=@Id";
                var req = (await connection.QueryAsync<Models.SheetAndTableName>(query, new { Id=Id})).FirstOrDefault();
                if (req!=null)
                {
                    Result = req.TableName;
                }
            }
            return Result;
        }

        public async Task<Guid> Insert<T>(T item)
        {
            using (var connection = new SqlConnection(connectionString)) return await connection.InsertAsync<Guid,T>(item);
        }

        public void TrackChanges(BaseSheet FromObject, BaseSheet ToObject)
        {
            PropertyInfo[] propertyInfos = typeof(BaseSheet).GetProperties();
            foreach (PropertyInfo prop in propertyInfos)
            {
                //пока реадизуем сравнение только строковых типов
                if (prop.PropertyType.Name=="String")
                  if (FromObject[prop.Name]!=null) 
                     if (ToObject[prop.Name] != FromObject[prop.Name])
                        ToObject[prop.Name] = FromObject[prop.Name];
            }
        }

        public async Task<bool> Update(BaseSheet item)
        {
            bool Result = false;
            //уточняем имя таблицы, которой принадлежит идентификтор
            string TableName = await GetTableNameById(item.Id);
            using (var connection = new SqlConnection(connectionString))
            {
                switch (TableName)
                {
                    case "Sheet1":
                        // Создание конфигурации сопоставления
                        var config1 = new MapperConfiguration(cfg => cfg.CreateMap<BaseSheet, Sheet1>());
                        // Настройка AutoMapper
                        var mapper1 = new Mapper(config1);
                        // сопоставление
                        var sheet1 = mapper1.Map<Sheet1>(item);
                        Result = (await connection.UpdateAsync<Sheet1>(sheet1)) > 0;
                        break;
                    case "Sheet2":
                        // Создание конфигурации сопоставления
                        var config2 = new MapperConfiguration(cfg => cfg.CreateMap<BaseSheet, Sheet2>());
                        // Настройка AutoMapper
                        var mapper = new Mapper(config2);
                        // сопоставление
                        var sheet2 = mapper.Map<Sheet2>(item);
                        Result = (await connection.UpdateAsync<Models.Sheet2>(sheet2)) > 0;
                        break;
                    default:
                        break;
                }
            }
            return Result;
        }
    }
}
