using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using siges.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace siges.Utilities
{
    public static class Tools
    {
        public static string QuitaSlash(string s)
        {
            if (s.Contains('/'))
            {
                StringBuilder nca = new StringBuilder();
                char[] arr = s.ToCharArray();
                foreach (char c in arr)
                {
                    if (c != '/' && c != ':')
                    {
                        nca.Append(c);
                    }
                }
                return nca.ToString();
            }
            return s;
        }

        public static bool EqualityByteArray(byte[] a1, byte[] b1)
        {
            int i;
            if (a1.Length == b1.Length)
            {
                i = 0;
                while (i < a1.Length && (a1[i] == b1[i]))
                {
                    i++;
                }
                if (i == a1.Length)
                {
                    return true;
                }
            }
            return false;
        }

        public static string OperadorNuevoEstadoStr(string estado)
        {
            if (!String.IsNullOrEmpty(estado))
            {
                if (estado == "sitio")
                    return "Sitio";
                if (estado == "noatendido")
                    return "RepAct";
                if (estado == "atendido")
                    return "Atendi";
                if (estado == "reporteInterno")
                    return "RepInt";
            }
            return "";
        }

        public static bool ValidaCamposCompletosOSAtendidos(List<Operador> osAtendido)
        {
            // Valida que esten capturados los campos necesarios para Notificar a los Usuarios, las actividades
            // a reportar de una Orden de Servicio. (List<Operador>)
            if (osAtendido != null && osAtendido.Count > 0)
                foreach (Operador atendido in osAtendido)
                {
                    if (String.IsNullOrEmpty(atendido.OrdenServicio.ContactoNombre))
                        return false;
                    if (String.IsNullOrEmpty(atendido.OrdenServicio.ContactoAP))
                        return false;
                    if (String.IsNullOrEmpty(atendido.OrdenServicio.ContactoEmail))
                        return false;
                    if (String.IsNullOrEmpty(atendido.OrdenServicio.Ubicacion.Nombre))
                        return false;
                    if (String.IsNullOrEmpty(atendido.OrdenServicio.Ubicacion.Contacto))
                        return false;
                    if (String.IsNullOrEmpty(atendido.OrdenServicio.Ubicacion.ContactoTelefono))
                        return false;
                    if (String.IsNullOrEmpty(atendido.Persona.Nombre))
                        return false;
                    if (String.IsNullOrEmpty(atendido.Persona.Paterno))
                        return false;
                    if (String.IsNullOrEmpty(atendido.Persona.Email))
                        return false;
                    return true;
                }
            return false;
        }

        public static Dictionary<int, string> ObtenerCamposFaltantesParaNotifica()
        {
            return null;
        }

        public static bool ValidaCamposCompletosOSyP(OrdenServicio os, Persona p)
        {
            // Valida que esten capturados los campos necesarios para Notificar a los Usuarios, las actividades
            // a reportar de una Orden de Servicio. (OrdenServicio)
            if (os != null)
            {
                if (String.IsNullOrEmpty(os.ContactoNombre))
                    return false;
                if (String.IsNullOrEmpty(os.ContactoAP))
                    return false;
                if (String.IsNullOrEmpty(os.ContactoEmail))
                    return false;
                if (String.IsNullOrEmpty(os.Ubicacion.Nombre))
                    return false;
                if (String.IsNullOrEmpty(os.Ubicacion.Contacto))
                    return false;
                if (String.IsNullOrEmpty(os.Ubicacion.ContactoTelefono))
                    return false;
                if (String.IsNullOrEmpty(p.Nombre))
                    return false;
                if (String.IsNullOrEmpty(p.Paterno))
                    return false;
                if (String.IsNullOrEmpty(p.Email))
                    return false;
                return true;
            }
            return false;
        }

        public static string ValidaEmail(string email)
        {
            if (!String.IsNullOrEmpty(email))
            {
                string pattern = @"((\w+)?(\w+)?[._-]?(\w+)?)*@((\w+)?[._-]?(\w+)?)*";
                Match match = Regex.Match(email, pattern, RegexOptions.IgnoreCase);
                if (match.Length > 7)
                {
                    return match.Value;
                }
                else
                {
                    Console.WriteLine("\n\tWARNING -> Posiblemente no se envíe correo a: {0}\n\t\t-> Resultado del filtro: {1}\n\t\t-> {2}\n", email, match.Value, DateTime.Now.ToString("f", CultureInfo.CreateSpecificCulture("es-ES")));
                    return match.Value;
                }
            }
            return "usuario@servidorcorreo.com.mx";
        }

        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
        
        //Utilities for ContactoClienteController
        public static int? CountUntilFindSlash(string word)
        {
            if (word.Contains("/"))
            {
                int count = 0;
                for (int i = (word.Length - 1); i >= 0; i--)
                {
                    count++;
                    if (word[i] == '/')
                    {
                        break;
                    }
                }
                return count;
            }
            else
            {
                return null;
            }
        }
        public static string GenerateToken()
        {
            TimeSpan epochTicks = new TimeSpan(new DateTime(1970, 1, 1).Ticks);
            TimeSpan unixTicks = new TimeSpan(DateTime.Now.Ticks) - epochTicks;
            Int32 unixTimestamp = (Int32)unixTicks.TotalSeconds;
            string token = Tools.GetSHA256(unixTimestamp.ToString());
            token = token.ToLower();
            return token;
        }
    }
}
