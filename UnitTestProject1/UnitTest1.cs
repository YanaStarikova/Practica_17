using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MusicStudio;
using System.Runtime.Remoting.Messaging;
using System.Data.SqlClient;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDate()
        {
            string dayweek = "Friday";
            string day = "";
            DateTime expected = DateTime.Today; 
            while (true) 
            {
                day = expected.DayOfWeek.ToString();
                if (dayweek == day)
                {
                    break;
                }
                else
                {
                    expected = expected.AddDays(1);
                }
            }
            DateTime actual = DateTime.Today.AddDays(1); //08.11.2024 (пт)
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_StartTimeGreaterThanEndTime()
        {
            string tStart = "10:00:00";
            string tEnd = "09:00:00";
            var validator = new TimeSpanValidator();
            var ex = Assert.ThrowsException<Exception>(() => validator.Test(tStart, tEnd));
            Assert.AreEqual(ex.Message,"Время начала должно быть меньше время конца!");
        }
        [TestMethod]
        public void Test_StartTimeEqualThanEndTime() 
        {
            string tStart = "09:00:00";
            string tEnd = "09:00:00";
            var validator = new TimeSpanValidator();
            var ex = Assert.ThrowsException<Exception>(() => validator.Test(tStart, tEnd));
            Assert.AreEqual(ex.Message, "Время начала должно быть меньше время конца!");
        }
        [TestMethod]
        public void Test_ValidTimes() 
        {
            string tStart = "09:00:00";
            string tEnd = "10:00:00";
            var validator = new TimeSpanValidator();
            var ex = Assert.ThrowsException<Exception>(() => validator.Test(tStart, tEnd));
            Assert.AreEqual(ex.Message, "Всё в порядке!");
        }
        [TestMethod]
        public void TestCheckClients_PasswordFalse()
        {
            string password = "111";
            var validator = new TimeSpanValidator();
            var ex = Assert.ThrowsException<Exception>(() => validator.PasswordClients(password));
            Assert.AreEqual(ex.Message, "Неверный пароль клиента!");
        }
        [TestMethod]
        public void TestCheckClients_PasswordTrue()
        {
            string password = "3456";
            var validator = new TimeSpanValidator();
            var ex =  validator.PasswordClients(password);
            Assert.AreEqual(ex, "Пароль верный!");
        }
        [TestMethod]
        public void TestCheckTeacher_PasswordFalse()
        {
            string password = "111";
            var validator = new TimeSpanValidator();
            var ex = Assert.ThrowsException<Exception>(() => validator.PasswordTeacher(password));
            Assert.AreEqual(ex.Message, "Неверный пароль учителя!");
        }
        [TestMethod]
        public void TestCheckTeacher_PasswordTrue()
        {
            string password = "1234";
            var validator = new TimeSpanValidator();
            var ex = validator.PasswordTeacher(password);
            Assert.AreEqual(ex, "Пароль верный!");
        }
        [TestMethod]
        public void TestGetClientData()
        {
            string password = "3456";
            string login = "user_1";
            int ex = 0;
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True"; //строка подключения
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = $"SELECT id_client FROM Clients Where password ='{password}' and login = '{login}';"; //запрос на получение данных
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                ex = reader.GetInt32(0);
            }
            reader.Close();
            connection.Close();

            int actual = 1;
            Assert.AreEqual(ex, actual);

        }
        [TestMethod]
        public void TestGetTeacherData()
        {
            string password = "1234";
            string login = "teacher_3";
            int ex = 0;
            string connectionString = @"Data Source=ADCLG1;Initial Catalog=practica_17;Integrated Security=True"; //строка подключения
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string selectQuery = $"SELECT id_teacher FROM Teachers Where password = '{password}' and login = '{login}';"; //запрос на получение данных
            SqlCommand command = new SqlCommand(selectQuery, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                ex = reader.GetInt32(0);
            }
            reader.Close();
            connection.Close();

            int actual = 3;
            Assert.AreEqual(ex, actual);
        }
    }
    public class TimeSpanValidator 
    {
        public void Test(string startTime, string endTime) //тестируемый метод
        {
            TimeSpan timeStart = TimeSpan.Parse(startTime);
            TimeSpan timeEnd = TimeSpan.Parse(endTime);
            if (timeStart >= timeEnd)
            {
                throw new Exception("Время начала должно быть меньше время конца!");
            }
            else
            {
                throw new Exception("Всё в порядке!");
            }
        }
        public string PasswordClients(string p)
        {
            if (p != "3456")
            {
                throw new Exception("Неверный пароль клиента!");
            }
            return "Пароль верный!";
        }
        public string PasswordTeacher(string p)
        {
            if (p != "1234")
            {
                throw new Exception("Неверный пароль учителя!");
            }
            return "Пароль верный!";
        }
    }
}

