using System;
using System.Net.Mail;

namespace siges.Models{
    public class Notificacion{
        public int Id {get; set;}
        public MailMessage Mensaje {get; set;}
        public int OperadorId {get; set;}
        public bool Confirmado {get; set;}
        public DateTime FechaConf {get; set;}
        public DateTime FechaEnv {get; set;}
    }
}
