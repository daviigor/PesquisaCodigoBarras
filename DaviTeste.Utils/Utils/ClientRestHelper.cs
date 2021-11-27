using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace DaviTeste.Utils.Utils
{
    public static class ClientRestHelper
    {
        #region variáveis
        public static ConfiguracaoClientRestModel _CONFIG_CLIENT { get; set; }
        #endregion

        #region métodos para requisição
        public static Object PUT<T>(List<T> value)
        {
            return Request<T>(value, Method.PUT);
        }
        public static Object POST<T>(List<T> value)
        {
            return Request<T>(value, Method.POST);
        }
        public static String GET()
        {
            return Request(Method.GET);
        }
        public static Object DELETE<T>(List<T> value)
        {
            return Request<T>(value, Method.DELETE);
        }

        private static Object Request<T>(List<T> value, Method method)
        {
            string payload = JsonHelper.serialize<T>(value, _CONFIG_CLIENT.IS_LIST_DATA);
            _CONFIG_CLIENT.REQUEST_CONTENT = payload;

            return Request(method);
        }

        private static String Request(Method method)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

            RestRequest request = getRequest(method);
            RestClient client = getClient(request);
            request.Timeout = 300000;

            IRestResponse response;
            response = client.Execute(request);
            _CONFIG_CLIENT.REQUEST_CONTENT = string.Empty;

            return response.Content;
        }
        #endregion

        #region métodos auxiliares
        private static RestClient getClient(RestRequest request)
        {
            RestClient client = new RestClient(_CONFIG_CLIENT.END_POINT);
            getAuthentication(client, request);

            return client;
        }

        private static RestRequest getRequest(Method method)
        {
            RestRequest request = new RestRequest(method);

            request.AddHeader("Cache-Control", _CONFIG_CLIENT.CACHE_CONTROL);
            validarConfiguracoes();

            return request;
        }

        private static void getAuthentication(RestClient client, RestRequest request)
        {
            if (!string.IsNullOrEmpty(_CONFIG_CLIENT.TOKEN))
            {
                request.AddHeader("X-Cosmos-Token", _CONFIG_CLIENT.TOKEN);

                return;
            }

            return;
        }

        #endregion

        #region validações
        private static void validarConfiguracoes()
        {
            if (string.IsNullOrEmpty(_CONFIG_CLIENT.END_POINT))
                throw new Exception("Endpoint/URL não configurada!");
        }

        #endregion
    }

    public class ConfiguracaoClientRestModel
    {
        public DateTime TOKEN_INIT { get; set; }
        public string TOKEN { get; set; }
        public string URL { get; set; }
        public string END_POINT { get; set; }
        public string CONTENT_TYPE { get; set; }
        public string CACHE_CONTROL { get; set; }
        public string REQUEST_CONTENT { get; set; }
        public string RESPONSE { get; set; }
        public bool IS_LIST_DATA { get; set; }
    }
}
