using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Talisman.Domain.Abstract;
using Talisman.Domain.Entities;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Reflection;

namespace Talisman.Domain.Concrete
{
    public class GlobalData
    {
        public List<Tovar> Tovars { get; set; }
        public List<Categorie> Categories { get; set; }
        public List<Article> Articles { get; set; }
        public List<ArtMin> ArtMins { get; set; }
        public List<Photo> Photos { get; set; }
        public List<Service> Services { get; set; }
        public List<FeedBack> FeedBacks { get; set; }
        public List<New> News { get; set; }
        public List<TovarsAndPhoto> TaP { get; set; } 
        public Over Over { get; set; }
       // public IEnumerable<Photo> Images { get; set; }
    }

    public class ApiDb2 
    {
        private string connectionString;
        public ApiDb2()
        {
            

        }

        public async Task<Result> Get(string name,string price)
        {            
            string sqlExpression = "dbo.sp_GetPrices";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;                
                SqlParameter p = new SqlParameter
                {
                    ParameterName = "@TovarName",
                    Value = name
                };
                command.Parameters.Add(p);
                SqlParameter p2 = new SqlParameter
                {
                    ParameterName = "@Price",
                    Value = price
                };
                command.Parameters.Add(p2);                
                var outSucc = new SqlParameter
                {
                    ParameterName = "@Success",
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.Bit
                };
                command.Parameters.Add(outSucc);
                var outErrMess = new SqlParameter
                {
                    ParameterName = "@ErrorMessage",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 100,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outErrMess);
                await command.ExecuteNonQueryAsync();
                return new Result((bool)command.Parameters["@Success"].Value, (string)command.Parameters["@ErrorMessage"].Value);
            }
        }

        public async Task<Result> Ins(string name, string price)
        {
            string sqlExpression = "dbo.sp_InsPrices";
            SqlCommand command=null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                 command= new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                SqlParameter p = new SqlParameter
                {
                    ParameterName = "@TovarName",
                    Value = name
                };
                command.Parameters.Add(p);
                SqlParameter p2 = new SqlParameter
                {
                    ParameterName = "@Price",
                    Value = price
                };
                command.Parameters.Add(p2);
                var outSucc = new SqlParameter
                {
                    ParameterName = "@Success",
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.Bit
                };
                command.Parameters.Add(outSucc);
                var outErrMess = new SqlParameter
                {
                    ParameterName = "@ErrorMessage",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 100,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outErrMess);
                await command.ExecuteNonQueryAsync();                
            }
            if (command != null)
            {
                return new Result((bool)command.Parameters["@Success"].Value, (string)command.Parameters["@ErrorMessage"].Value);
            }
            else
            {
                return new Result(false,"Что-то пошло не так");
            }
        }

        public async Task<Result> ReadPhotoIds(int id)  
        {
            List<int> result = null;
            Result res = null;
            string sqlExpression = "dbo.sp_ReadPhIds";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter p = new SqlParameter
                        {
                            ParameterName = "@Id",
                            Value = id,
                        };
                    command.Parameters.Add(p);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows) // если есть данные
                    {
                        result = new List<int>();
                        while (await reader.ReadAsync()) // построчно считываем данные
                        {
                            result.Add(reader.GetInt32(0));
                        }
                    }
                }
                if (result != null)
                {
                    res = new Result(true, null, result);
                }
                else
                {
                    res = new Result(false, "Фото не найдены");
                }
            }
            catch(Exception exc)
            {
                res = new Result(false, exc.Message);
            }
            return res;
        }

        public async Task<Result> ReadTovDesc(int id) 
        {
            string[] result = new string[4];
            Result res = null;
            string sqlExpression = "dbo.sp_ReadTovDesc";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter p = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = id,
                    };
                    command.Parameters.Add(p);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows) // если есть данные
                    {
                        //result = new List<int>();
                        while (await reader.ReadAsync()) // построчно считываем данные
                        {
                            //result.Add(reader.GetInt32(0));
                            result[0] = reader.GetString(0);
                            result[1] = reader.GetString(1);
                            result[2] = reader.GetString(2);
                            result[3] = reader.GetString(3);
                        }
                    }
                }
                if (result != null)
                {
                    res = new Result(true, null, result);
                }
                else
                {
                    res = new Result(false, "Описание не найдено");
                }
            }
            catch (Exception exc)
            {
                res = new Result(false, exc.Message);
            }
            return res;
        }

        public async Task<Result> ReadArtCont(int id)
        {
            string result = null;
            Result res = null;
            string sqlExpression = "dbo.sp_ReadArtCont";
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    SqlCommand command = new SqlCommand(sqlExpression, connection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter p = new SqlParameter
                    {
                        ParameterName = "@Id",
                        Value = id,
                    };
                    command.Parameters.Add(p);
                    SqlDataReader reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows) // если есть данные
                    {
                        //result = new List<int>();
                        while (await reader.ReadAsync()) // построчно считываем данные
                        {
                            //result.Add(reader.GetInt32(0));
                            result = reader.GetString(0);
                        }
                    }
                }
                if (result != null)
                {
                    res = new Result(true, null, result);
                }
                else
                {
                    res = new Result(false, "Содержимое не найдено");
                }
            }
            catch (Exception exc)
            {
                res = new Result(false, exc.Message);
            }
            return res;
        }
    }

    public class ApiDb
    {
        private string connectionString;
        public ApiDb()
        {
            connectionString = ConfigurationManager.ConnectionStrings["EFDBContext"].ConnectionString;
        }

        

        public async Task<Result> Ins<T>(T o) 
        {
            string sqlExpression = "dbo.sp_Ins" + typeof(T).Name+"s";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType= System.Data.CommandType.StoredProcedure;
                //Надо определить параметры для хранимой процедуры. Сделаем это с помощью рефлексии.
                var t = o.GetType();
                //определяем входные параметры
                //bool f = true;
                foreach (var x in t.GetProperties())
                {                    
                    if (x.GetCustomAttribute(typeof(System.ComponentModel.DataAnnotations.KeyAttribute), false) != null)                    
                    {
                        //f = false;
                        continue;
                    }                    
                    SqlParameter p = new SqlParameter
                    {
                        ParameterName = "@" + x.Name,
                        Value = x.GetValue(o),
                        Direction= ParameterDirection.Input,
                         
                    };
                    command.Parameters.Add(p);
                }
                //определяем выходные параметры(для любого типа одинаковые)
                var outId = new SqlParameter
                {
                    ParameterName = "@Id",
                    SqlDbType = SqlDbType.Int,
                    Direction= ParameterDirection.Output
                };
                command.Parameters.Add(outId);
                var outSucc = new SqlParameter
                {
                    ParameterName = "@Success",
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.Bit
                };
                command.Parameters.Add(outSucc);
                var outErrMess = new SqlParameter
                {
                    ParameterName = "@ErrorMessage",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Output,
                    Size=100
                };
                command.Parameters.Add(outErrMess);

                await command.ExecuteNonQueryAsync();

                return new Result((bool)(command.Parameters["@Success"].Value), (string)(command.Parameters["@ErrorMessage"].Value), command.Parameters["@Id"].Value);
            }
        }

        public async Task<Result> Upd<T>(T o) 
        {
            string sqlExpression = "dbo.sp_Upd" + typeof(T).Name + "s";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //Надо определить параметры для хранимой процедуры. Сделаем это с помощью рефлексии.
                var t = o.GetType();
                //определяем входные параметры               
                foreach (var x in t.GetProperties())
                {                    
                    SqlParameter p = new SqlParameter
                    {
                        ParameterName = "@" + x.Name,
                        Value = x.GetValue(o)
                    };
                    command.Parameters.Add(p);
                }
                //определяем выходные параметры(для любого типа одинаковые)
                
                var outSucc = new SqlParameter
                {
                    ParameterName = "@Success",
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.Bit
                };
                command.Parameters.Add(outSucc);
                var outErrMess = new SqlParameter
                {
                    ParameterName = "@ErrorMessage",
                    SqlDbType = SqlDbType.NVarChar,
                    Size=100,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outErrMess);

                await command.ExecuteNonQueryAsync();

                return new Result((bool)command.Parameters["@Success"].Value, (string)command.Parameters["@ErrorMessage"].Value);
            }
        }
        public async Task<Result> Del<T>(int index) 
        {
            var t = typeof(T);
            string sqlExpression = "dbo.sp_Del" + t.Name + "s";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                //Надо определить параметры для хранимой процедуры. Сделаем это с помощью рефлексии.
                
                //определяем входные параметры               
                //foreach (var x in t.GetProperties())
                //{
                    SqlParameter p = new SqlParameter
                    {
                        ParameterName = "@Id",//x.Name,
                        Value = index
                    };
                    command.Parameters.Add(p);
                //}
                //определяем выходные параметры(для любого типа одинаковые)

                var outSucc = new SqlParameter
                {
                    ParameterName = "@Success",
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.Bit
                };
                command.Parameters.Add(outSucc);
                var outErrMess = new SqlParameter
                {
                    ParameterName = "@ErrorMessage",
                    SqlDbType = SqlDbType.NVarChar,
                    Size=100,
                    Direction = ParameterDirection.Output
                };
                command.Parameters.Add(outErrMess);
                await command.ExecuteNonQueryAsync();
                return new Result((bool)command.Parameters["@Success"].Value, (string)command.Parameters["@ErrorMessage"].Value);
            }
        }
        public async Task<List<T>> Get<T>() where T:new()
        {
            //var cs = ConfigurationManager.ConnectionStrings["EFDBContext"].ConnectionString;
            var t = typeof(T);
            var r = new List<T>();
            string sqlExpression = "dbo.sp_Get"+t.Name+"s";
            SqlCommand command = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();
                command = new SqlCommand(sqlExpression, connection);
                
                SqlDataReader reader = await command.ExecuteReaderAsync();
                
                if (reader.HasRows) // если есть данные
                {
                    while (await reader.ReadAsync()) // построчно считываем данные
                    {
                        T o = new T();
                        
                        foreach (var x in t.GetProperties())
                        {
                            try
                            {
                                x.SetValue(o, reader.GetValue(reader.GetOrdinal(x.Name)));
                            }
                            catch
                            {
                                //x.SetValue(o, default(x.GetType());
                            }
                        }
                        r.Add(o);
                    }

                }
                reader.Close();
            }
            return r;
        }
        public List<T> Gets<T>() where T : new() //НЕАССИНХРОННЫЙ МЕТОД
        {
            //var cs = ConfigurationManager.ConnectionStrings["EFDBContext"].ConnectionString;
            var t = typeof(T);
            var r = new List<T>();
            string sqlExpression = "dbo.sp_Get" + t.Name + "s";
            SqlCommand command = null;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command = new SqlCommand(sqlExpression, connection);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows) // если есть данные
                {
                    while (reader.Read()) // построчно считываем данные
                    {
                        T o = new T();

                        foreach (var x in t.GetProperties())
                        {
                            try
                            {
                                x.SetValue(o, reader.GetValue(reader.GetOrdinal(x.Name)));
                            }
                            catch
                            {
                                //x.SetValue(o, default(x.GetType());
                            }
                        }
                        r.Add(o);
                    }

                }
                reader.Close();
            }
            return r;
        }
    }

    //public class GData //: IGlobalData
    //{
    //    public GData()
    //    {

    //    }

    //    public List<TovarsAndPhoto> TaP  
    //    {
    //        get { return (HttpContext.Current.Application["GlobalData"] as GlobalData).TaP; }
    //    }

    //    public List<Tovar> Tovars
    //    {
    //        get { return (HttpContext.Current.Application["GlobalData"] as GlobalData).Tovars; }
    //    }

    //    public List<Categorie> Categories
    //    {
    //        get { return (HttpContext.Current.Application["GlobalData"] as GlobalData).Categories; }
    //    }

    //    public List<Article> Articles
    //    {
    //        get { return (HttpContext.Current.Application["GlobalData"] as GlobalData).Articles; }
    //    }

    //    public List<Photo> Photos
    //    {
    //        get { return (HttpContext.Current.Application["GlobalData"] as GlobalData).Photos; }
    //    }



    //    public List<Service> Services
    //    {
    //        get { return (HttpContext.Current.Application["GlobalData"] as GlobalData).Services; }
    //    }

    //    public List<FeedBack> FeedBacks
    //    {
    //        get { return (HttpContext.Current.Application["GlobalData"] as GlobalData).FeedBacks; }
    //    }

    //    public List<New> News
    //    {
    //        get { return (HttpContext.Current.Application["GlobalData"] as GlobalData).News; }
    //    }


    //    public Over Over
    //    {
    //        get { return (HttpContext.Current.Application["GlobalData"] as GlobalData).Over; }
    //    }
    //}
    public enum HeadersFor { Index, Category, Position, Services, Articles, Feedbacks, Contacts}

    public class GData3 : IGlobalData 
    {
        private ApiDb api;
        public object loc = new object();
        public GData3()
        {            
            api = new ApiDb();
        }

        public DateTime Get_LM(HeadersFor headerFor, int id=1)
        {
            DateTime rez = Over.LM;
            switch (headerFor)
            {
                case(HeadersFor.Index):
                    var lm = TaP.Where(t => t.IsNew).Select(t => t.LM).Max();
                    if (Over.LM > lm)
                    {
                        rez = Over.LM;
                    }
                    else{
                        rez=lm;
                    }
                    break;
                case(HeadersFor.Category):
                    var lm2 = TaP.Where(t => t.CategoryId == id).Select(t => t.LM).Max();
                    if (Over.LM > lm2)
                    {
                        rez = Over.LM;
                    }
                    else{
                        rez=lm2;
                    }
                    break;
                case(HeadersFor.Position):
                    var lm3 = TaP.Where(t => t.TovarId == id).Select(t => t.LM).Max();
                    if (Over.LM > lm3)
                    {
                        rez = Over.LM;
                    }
                    else{
                        rez=lm3;
                    }
                    break;
                case(HeadersFor.Services):
                    var lm4 = Services.Select(s => s.LM).Max();
                    if (Over.LM > lm4)
                    {
                        rez = Over.LM;
                    }
                    else{
                        rez=lm4;
                    }
                    break;
                case(HeadersFor.Feedbacks):
                    var lm5 = FeedBacks.Select(f => f.Date).Max();
                    if (Over.LM > lm5)
                    {
                        rez = Over.LM;
                    }
                    else{
                        rez=lm5;
                    }
                    break;                    
            }
            return rez;
        }

        public List<ArtMin> ArtMins
        {
            get
            {
                if ((HttpContext.Current.Application["GlobalData"] as GlobalData).ArtMins == null)
                {
                    var l = api.Gets<ArtMin>();
                    (HttpContext.Current.Application["GlobalData"] as GlobalData).ArtMins = l;
                    return l;
                }
                return (HttpContext.Current.Application["GlobalData"] as GlobalData).ArtMins;
            }
        }

        public List<TovarsAndPhoto> TaP 
        {
            get 
            {
                if ((HttpContext.Current.Application["GlobalData"] as GlobalData).TaP == null)
                {
                    var l = api.Gets<TovarsAndPhoto>();
                    (HttpContext.Current.Application["GlobalData"] as GlobalData).TaP = l;
                    return l;
                }
                return (HttpContext.Current.Application["GlobalData"] as GlobalData).TaP; 
            }
        }

        public List<Tovar> Tovars
        {
            get 
            {
                if ((HttpContext.Current.Application["GlobalData"] as GlobalData).Tovars == null)
                {
                    var l = api.Gets<Tovar>();
                    (HttpContext.Current.Application["GlobalData"] as GlobalData).Tovars = l;
                    return l;
                }
                return (HttpContext.Current.Application["GlobalData"] as GlobalData).Tovars; 
            }
        }

        public List<Categorie> Categories
        {
            get 
            {
                
                lock (loc)
                {
                    if ((HttpContext.Current.Application["GlobalData"] as GlobalData).Categories == null)
                    {
                       // Monitor.Wait(loc);
                        (HttpContext.Current.Application["GlobalData"] as GlobalData).Categories = api.Gets<Categorie>();
                        //Monitor.Pulse(loc);
                        //GetCat().Wait();

                    }
                    return (HttpContext.Current.Application["GlobalData"] as GlobalData).Categories;
                }
            }
        }
       
        public List<Article> Articles
        {
            get 
            {
                if ((HttpContext.Current.Application["GlobalData"] as GlobalData).Articles == null)
                {
                    (HttpContext.Current.Application["GlobalData"] as GlobalData).Articles = api.Gets<Article>();
                }
                return (HttpContext.Current.Application["GlobalData"] as GlobalData).Articles; 
            }
        }

        public List<Photo> Photos
        {
            get 
            {
                if ((HttpContext.Current.Application["GlobalData"] as GlobalData).Photos == null)
                {
                    (HttpContext.Current.Application["GlobalData"] as GlobalData).Photos = api.Gets<Photo>();
                }
                return (HttpContext.Current.Application["GlobalData"] as GlobalData).Photos; 
            }
        }



        public List<Service> Services
        {
            get 
            {
                if ((HttpContext.Current.Application["GlobalData"] as GlobalData).Services == null)
                {
                    (HttpContext.Current.Application["GlobalData"] as GlobalData).Services = api.Gets<Service>();
                }
                return (HttpContext.Current.Application["GlobalData"] as GlobalData).Services; 
            }
        }

        public List<FeedBack> FeedBacks
        {
            get 
            {
                if ((HttpContext.Current.Application["GlobalData"] as GlobalData).FeedBacks == null)
                {
                    (HttpContext.Current.Application["GlobalData"] as GlobalData).FeedBacks = api.Gets<FeedBack>();
                }
                return (HttpContext.Current.Application["GlobalData"] as GlobalData).FeedBacks; 
            }
        }

        public List<New> News
        {
            get 
            {
                //var l = (HttpContext.Current.Application["GlobalData"] as GlobalData).News;
                if ((HttpContext.Current.Application["GlobalData"] as GlobalData).News == null)
                {
                    (HttpContext.Current.Application["GlobalData"] as GlobalData).News = api.Gets<New>();
                }
                return (HttpContext.Current.Application["GlobalData"] as GlobalData).News; 
            }
        }


        public Over Over
        {
            get 
            {
                //var l = (HttpContext.Current.Application["GlobalData"] as GlobalData).Over;
                if ((HttpContext.Current.Application["GlobalData"] as GlobalData).Over == null)
                {
                    (HttpContext.Current.Application["GlobalData"] as GlobalData).Over = api.Gets<Over>().FirstOrDefault();
                }
                return (HttpContext.Current.Application["GlobalData"] as GlobalData).Over; 
            }
        }        
    }    
}



 