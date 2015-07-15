using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace BDExample1
{
    public partial class Form1 : Form
    {
        public MySqlConnection Connection;
        public DataTable dataTabel;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder ConnectionString = new MySqlConnectionStringBuilder(); //Создаем объект строки подклбчения к БД.
                                                                                                //Можем сразу написать строку подключения
                                                                                                //или вбить параметры отдельно. Я вбиваю отдельно
            ConnectionString.Server = "127.0.0.1"; //здесь пишем адрес сервера с базой данных.
                                                   //Так как наш сервер находтся на этом компьютере
                                                   //пишем такой адрес

            ConnectionString.Database = "test"; //Имя базы к кторой подключаемся
            ConnectionString.UserID = textBox1.Text;  //Имя пользователя. Здесь нуно будетм указать свое имя пользователя и свйо пароль.
            ConnectionString.Password = textBox2.Text;//Пароль ползователя 

            Connection = new MySqlConnection(ConnectionString.GetConnectionString(true)); //Создаем объект подключения к БД. В касестве ппраметра
                                                                                          //конструктору передаем строку подключение с методом GetConnectionString(true)
                                                                                          //True означет что мы преедаем строку подключения вместе с паролем
            button2.Enabled = true;
            button3.Enabled = true;
            button4.Enabled = true;
            button1.Enabled = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string queryString = @"SELECT * FROM tabtest"; //Создаем строку в которой пишем запрос в БД
            try
            {
                MySqlCommand Command = new MySqlCommand(queryString, Connection); //Создаем объект команды. В качестве параметра конструктору передаем
                //строку с запроса в бд и объект подключения к бд

                Connection.Open(); //Перед тем как предать запрос в бд открываем подключение,
                //вызывая у объекта Connection метод Open();
                MySqlDataReader dataReader = Command.ExecuteReader(); //Создавая объект dataReader с Помощью метода объекта Command.ExectueReader()
                //мы передеаем запрос в бд и записываем полченный ответ в dataReader

                dataTabel = new DataTable(); //Создаем таблицу чтобы сохрнить туда данные из запроса
                dataTabel.Load(dataReader); // Загружаем в нее данные из БД
                dataGridView1.DataSource = dataTabel; // Выводим в DataGridView
                Connection.Close(); //Закрываем подключение к БД
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.ToString()); //Если что то пойдет не так, то сможем посмотреть сообщение об ошибке asd asdas dasd sd 
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<string> NameList = new List<string>() {"A","B","C","D","E","F","G"};
            List<int> ValList = new List<int>() {1,2,3,4,5,6,7}; // Коллекции которые будем добавлять

            string qs = "INSERT INTO tabtest (name, val) VALUES (@name, @val)"; // пишем строку запросу, где (name, val) имена полей куда будем добавлять
                                                                                // а VALUES (@name, @val) значения которые будем добавять. СТавим @ чтобы потом добавить туда наши значения

            for (int i = 0; i < ValList.Count; i++) // каждую пару элементов коллекции записываем отдельно
            {
                Connection.Open(); // открываем подключение к БД
                MySqlCommand command = new MySqlCommand(qs, Connection); //формируем команду
                command.Parameters.AddWithValue("@name", NameList[i]);  
                command.Parameters.AddWithValue("@val", ValList[i]); // добавляем значения из колекции
                command.ExecuteNonQuery(); // выполняем запрос
                Connection.Close(); // закрываем подключение
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string qs = "CREATE TABLE `test`.`tabtest` (`id` INT NOT NULL AUTO_INCREMENT,`name` VARCHAR(45) NULL,`val` DOUBLE NULL,PRIMARY KEY (`id`));";
            MySqlCommand Command = new MySqlCommand(qs, Connection);
            Connection.Open();
            Command.ExecuteNonQuery();
            Connection.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
