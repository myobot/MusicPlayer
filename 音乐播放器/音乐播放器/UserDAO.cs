using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 音乐播放器
{
    class UserDAO
    {
        MySql.Data.MySqlClient.MySqlConnection msqlConnection = null;
        private static string constr= "server=localhost;user id=root;password=wang8970;database=csharpwork;charset=utf8;";
        public List<User> getUser() {
            List<User> l = new List<User>();
            msqlConnection = new MySql.Data.MySqlClient.MySqlConnection(constr);
            //define the command reference
            MySql.Data.MySqlClient.MySqlCommand msqlCommand = new MySql.Data.MySqlClient.MySqlCommand();
            //define the connection used by the command object
            msqlCommand.Connection = this.msqlConnection;
            //define the command text
            msqlCommand.CommandText = "SELECT user_name,password,image_num FROM user;";
            Console.WriteLine(msqlCommand.CommandText);
            try
            {
                //open the connection
                this.msqlConnection.Open();
                //use a DataReader to process each record
                msqlCommand.ExecuteNonQuery();
                MySql.Data.MySqlClient.MySqlDataReader msqlReader = msqlCommand.ExecuteReader();
                while (msqlReader.Read())
                {
                    User u = new User();
                    u.username = msqlReader[1].ToString();
                    u.password = msqlReader[2].ToString();
                    u.imagenum = int.Parse(msqlReader[3].ToString());
                    l.Add(u);
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
                //do something with the exception
            }
            finally
            {
                //always close the connection
                this.msqlConnection.Close();
            }
            return l;
        }
        public int getUserImg(String username)
        {
            int UserImg = 0;
            msqlConnection = new MySql.Data.MySqlClient.MySqlConnection(constr);
            //define the command reference
            MySql.Data.MySqlClient.MySqlCommand msqlCommand = new MySql.Data.MySqlClient.MySqlCommand();
            //define the connection used by the command object
            msqlCommand.Connection = this.msqlConnection;
            //define the command text
            msqlCommand.CommandText = "SELECT image_num FROM user WHERE user_name='"+username+"';";
            Console.WriteLine(msqlCommand.CommandText);
            try
            {
                //open the connection
                this.msqlConnection.Open();
                //use a DataReader to process each record
                msqlCommand.ExecuteNonQuery();
                MySql.Data.MySqlClient.MySqlDataReader msqlReader = msqlCommand.ExecuteReader();
                if (msqlReader.HasRows)
                {
                    msqlReader.Read();
                    UserImg = int.Parse(msqlReader[0].ToString());
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
                //do something with the exception
            }
            finally
            {
                //always close the connection
                this.msqlConnection.Close();
            }
            return UserImg;
        }
        public bool finduser(String username,String password)
        {
            bool flag = false;
            msqlConnection = new MySql.Data.MySqlClient.MySqlConnection(constr);
            //define the command reference
            MySql.Data.MySqlClient.MySqlCommand msqlCommand = new MySql.Data.MySqlClient.MySqlCommand();
            //define the connection used by the command object
            msqlCommand.Connection = this.msqlConnection;
            //define the command text
            
            msqlCommand.CommandText = "SELECT user_name FROM user WHERE user_name='" + username + "' AND password='"+password+"';";
            Console.WriteLine(msqlCommand.CommandText);
            try
            {
                //open the connection
                this.msqlConnection.Open();
                //use a DataReader to process each record
                msqlCommand.ExecuteNonQuery();
                MySql.Data.MySqlClient.MySqlDataReader msqlReader = msqlCommand.ExecuteReader();
                if (msqlReader.HasRows)
                {
                    flag = true;
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
                //do something with the exception
            }
            finally
            {
                //always close the connection
                this.msqlConnection.Close();
            }
            return flag;

        }
        public bool finduser(String username)
        {
            bool flag = false;
            msqlConnection = new MySql.Data.MySqlClient.MySqlConnection(constr);
            //define the command reference
            MySql.Data.MySqlClient.MySqlCommand msqlCommand = new MySql.Data.MySqlClient.MySqlCommand();
            //define the connection used by the command object
            msqlCommand.Connection = this.msqlConnection;
            //define the command text

            msqlCommand.CommandText = "SELECT user_name FROM user WHERE user_name='" + username + "';";
            Console.WriteLine(msqlCommand.CommandText);
            try
            {
                //open the connection
                this.msqlConnection.Open();
                //use a DataReader to process each record
                msqlCommand.ExecuteNonQuery();
                MySql.Data.MySqlClient.MySqlDataReader msqlReader = msqlCommand.ExecuteReader();
                if (msqlReader.HasRows)
                {
                    flag = true;
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
                //do something with the exception
            }
            finally
            {
                //always close the connection
                this.msqlConnection.Close();
            }
            return flag;

        }
        public List<String> getUserLikeList(String username)
        {
            List<String> l = new List<string>();
            msqlConnection = new MySql.Data.MySqlClient.MySqlConnection(constr);
            //define the command reference
            MySql.Data.MySqlClient.MySqlCommand msqlCommand = new MySql.Data.MySqlClient.MySqlCommand();
            //define the connection used by the command object
            msqlCommand.Connection = this.msqlConnection;
            //define the command text
            msqlCommand.CommandText = "SELECT music_path FROM user_like_music_list WHERE user_name ='" + username + "';";
            Console.WriteLine(msqlCommand.CommandText);
            try
            {
                //open the connection
                this.msqlConnection.Open();
                //use a DataReader to process each record
                msqlCommand.ExecuteNonQuery();
                MySql.Data.MySqlClient.MySqlDataReader msqlReader = msqlCommand.ExecuteReader();
                while (msqlReader.Read())
                {
                    l.Add(msqlReader[0].ToString());
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
                //do something with the exception
            }
            finally
            {
                //always close the connection
                this.msqlConnection.Close();
            }
            
            return l;
        }
        public bool RegistUser(String username,String password,int imgnum)
        {
            bool flag = false;
            msqlConnection = new MySql.Data.MySqlClient.MySqlConnection(constr);
            //define the command reference
            MySql.Data.MySqlClient.MySqlCommand msqlCommand = new MySql.Data.MySqlClient.MySqlCommand();
            //define the connection used by the command object
            msqlCommand.Connection = this.msqlConnection;
            //define the command text
            msqlCommand.CommandText = "INSERT INTO user(user_name,password,image_num) VALUES('"+username+"','"+password+"','"+imgnum+"');";
            Console.WriteLine(msqlCommand.CommandText);
            try
            {
                //open the connection
                this.msqlConnection.Open();
                //use a DataReader to process each record
                int i= msqlCommand.ExecuteNonQuery();
                if (i>0)
                {
                    flag = true;
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
                //do something with the exception
            }
            finally
            {
                //always close the connection
                this.msqlConnection.Close();
            }
            return flag;
        }
        public String getPath(String path)
        {
            String[] s1 = path.Split('\\');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < s1.Length; i++)
            {
                sb.Append(s1[i]);
                if (i != s1.Length - 1)
                {
                    sb.Append("\\\\");
                }
            }
            return sb.ToString();
        }
        public void UpdateUser_image(String username,int image_num)
        {
            msqlConnection = new MySql.Data.MySqlClient.MySqlConnection(constr);
            //define the command reference
            MySql.Data.MySqlClient.MySqlCommand msqlCommand = new MySql.Data.MySqlClient.MySqlCommand();
            //define the connection used by the command object
            msqlCommand.Connection = this.msqlConnection;
            //define the command text
            msqlCommand.CommandText = "UPDATE user SET image_num='" + image_num + "' WHERE user_name='" + username + "';";
            Console.WriteLine(msqlCommand.CommandText);
            try
            {
                //open the connection
                this.msqlConnection.Open();
                //use a DataReader to process each record
                msqlCommand.ExecuteNonQuery();
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
                //do something with the exception
            }
            finally
            {
                //always close the connection
                this.msqlConnection.Close();
            }
    }
    public void UpdateLikeMusic(List<String> likemusiclis,String username)
        {
            msqlConnection = new MySql.Data.MySqlClient.MySqlConnection(constr);
            //define the command reference
            MySql.Data.MySqlClient.MySqlCommand msqlCommand = new MySql.Data.MySqlClient.MySqlCommand();
            //define the connection used by the command object
            msqlCommand.Connection = this.msqlConnection;
            //define the command text
            msqlCommand.CommandText = "DELETE FROM user_like_music_list WHERE user_name='"+username+"';";
            Console.WriteLine(msqlCommand.CommandText);
            try
            {
                //open the connection
                this.msqlConnection.Open();
                //use a DataReader to process each record
                msqlCommand.ExecuteNonQuery();
                for (int j = 0; j < likemusiclis.Count; j++)
                {
                    msqlCommand.CommandText = "INSERT INTO user_like_music_list(user_name,music_path) VALUES('" + username + "','" + getPath(likemusiclis[j]) + "');";
                    
                    Console.WriteLine(msqlCommand.CommandText);
                    msqlCommand.ExecuteNonQuery();
                }
            }
            catch (Exception er)
            {
                Console.WriteLine(er.Message);
                //do something with the exception
            }
            finally
            {
                //always close the connection
                this.msqlConnection.Close();
            }
        }
    }
}
