using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;
using System.Configuration;

namespace AMRS.Models{
    public class AMRSDBContext
    {

        static string connstr = "Server=localhost;User ID=admin;Password=admin123!;Port=3306;Database=fastair";
        public string ConnectionString { get; set; }
        public AMRSDBContext(string connectionString)
        {
            this.ConnectionString = connstr;
        }
      
    
        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        /// <summary>
        /// Get List of Airport
        /// </summary>
        /// <returns></returns>
        public List<AirportsModel> GetAllAirports()
        {
            List<AirportsModel> list = new List<AirportsModel>();
        
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string airport_sp = "GetAirports";
                MySqlCommand cmd = new MySqlCommand(airport_sp, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AirportsModel(){
                            portId = reader.GetInt32("portid"),
                            portName = reader.GetString("portname"),
                            portCity = reader.GetString("portcity"),
                            portCountry = reader.GetString("portcountry"),
                            portZip = reader.GetString("portzip"),
                        });
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// Search Airport By Name
        /// </summary>
        /// <param name="strPortName"></param>
        /// <returns></returns>
        public List<AirportsModel> SearchAirport(string strPortName)
        {
            List<AirportsModel> list = new List<AirportsModel>();
        
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string airport_sp = "Search_Airport";
                MySqlCommand cmd = new MySqlCommand(airport_sp, conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_portName", strPortName);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new AirportsModel(){
                            portId = reader.GetInt32("portid"),
                            portName = reader.GetString("portname"),
                            portCity = reader.GetString("portcity"),
                            portCountry = reader.GetString("portcountry"),
                            portZip = reader.GetString("portzip"),
                        });
                    }
                }
            }
                return list;
        }



        /// <summary>
        /// Insert New Airport 
        /// </summary>
        /// <param name="airportsModel"></param>
        /// <returns></returns>
        public int AddAirport(AirportsModel airportsModel)
        {
            try 
            {
                int retId = 0;

                string strPortName = airportsModel.portName;
                string strPortCountry = airportsModel.portCountry;
                string strPortCity = airportsModel.portCity;
                string strPortZip = airportsModel.portZip;

                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string airport_sp = "AddAirport";
                    MySqlCommand cmd = new MySqlCommand(airport_sp, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@portName", strPortName);
                    cmd.Parameters.AddWithValue("@portCity", strPortCity);
                    cmd.Parameters.AddWithValue("@portCountry", strPortCountry);
                    cmd.Parameters.AddWithValue("@portZip", strPortZip);

                    retId = cmd.ExecuteNonQuery();

                }
                    return retId;
            }
            catch(MySqlException sqlExp)
            {
                return sqlExp.ErrorCode;
            }
        }

        /// <summary>
        /// Return Aipport details based on ID
        /// </summary>
        /// <param name="portId"></param>
        /// <returns></returns>
        public AirportsModel GetById_Airport(int portId)
        {
               
            AirportsModel obj = null; 

                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string airport_sp = "GetByID_Airport";
                    MySqlCommand cmd = new MySqlCommand(airport_sp, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@_portId", portId);
                    
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {   
                                if (reader.Read())
                                {
                                   obj = new AirportsModel()
                                    {
                                        portId = reader.GetInt32("portid"),
                                        portName = reader.GetString("portname"),
                                        portCity = reader.GetString("portcity"),
                                        portCountry = reader.GetString("portcountry"),
                                        portZip = reader.GetString("portzip"),
                                    };
                        }
                    }
                }

                return obj;
        }

        /// <summary>
        /// Update Airport Entry by ID
        /// </summary>
        /// <param name="airportsModel"></param>
        /// <returns></returns>
        public int UpdateAirport(AirportsModel airportsModel)
        {
            try 
            {
                int retId = 0;

                int portId = airportsModel.portId;
                string strPortName = airportsModel.portName;
                string strPortCountry = airportsModel.portCountry;
                string strPortCity = airportsModel.portCity;
                string strPortZip = airportsModel.portZip;

                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string airport_sp = "Update_Airport";
                    MySqlCommand cmd = new MySqlCommand(airport_sp, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    
                    cmd.Parameters.AddWithValue("@airportID", portId);
                    cmd.Parameters.AddWithValue("@portName", strPortName);
                    cmd.Parameters.AddWithValue("@portCity", strPortCity);
                    cmd.Parameters.AddWithValue("@portCountry", strPortCountry);
                    cmd.Parameters.AddWithValue("@portZip", strPortZip);

                    retId = cmd.ExecuteNonQuery();

                }
                    return retId;
            }
            catch(MySqlException sqlExp)
            {
                return sqlExp.ErrorCode;
            }
        }

        /// <summary>
        /// Delete Airport based on portID
        /// </summary>
        /// <param name="portId"></param>
        /// <returns></returns>
        public int DeleteAirport(int portId)
        {
             try 
            {
                
                int retId = 0; 

                using (MySqlConnection conn = GetConnection())
                {
                    conn.Open();
                    string airport_sp = "Delete_Airport";
                    MySqlCommand cmd = new MySqlCommand(airport_sp, conn);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@airportId", portId);
                    retId = cmd.ExecuteNonQuery();
                }
                    return retId;
            }
            catch(MySqlException sqlExp)
            {
                return sqlExp.ErrorCode;
            }
        }     

    }
}
