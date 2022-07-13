using System;
using System.Collections.Generic;
using System.Text;

namespace LiveDataProcessing
{
    internal class DataHandler
    {
        private List<string> cityList;

        private List<string> queryList;
        public DataHandler(List<string> cityList, string apiKey)
        {
            this.cityList = cityList;
            this.queryList = new List<string>();
            for (int i = 0; i < cityList.Count; i++)
            {
                string query = "http://api.weatherapi.com/v1/current.json?key=" + apiKey + "&q=" + cityList[i] + "&aqi=no";
                queryList.Add(query);
            }
        }
        public List<string> CityList
        {
            get
            {
                return this.cityList;
            }
        }

        public List<string> QueryList
        {
            get
            {
                return this.queryList;
            }
        }
    }
}
