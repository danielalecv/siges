namespace siges.Utilities
{
    public class Constants
    {
        public class Home
        {
            public static string SuccessfulEmailSending = "Hemos recibido tu problematica, trabajaremos en la solución";
            public static string FailEmailSending = "Hubo un error al enviar tu problema, intenta de nuevo";
        }

        public class Manage
        {
            public static string Title = "Datos Personales";
            public static string UnableToLoadUser = "Unable to load user with ID";
            public static string ValidateGeneralDirection = "Debe completar todos los datos (General/Direccíon)";
            public static string UnexpectedErrorPhoneNumber = "Unexpected error occurred setting phone number for user with ID";
            public static string SuccessfulUpdate = "Su información ha sido actualizada.";
        }
        public class ContactoCliente
        {
            public static string ConfirmSuccess = "Gracias por confirmar.\n Su correo electrónico será utilizado como contacto del cliente ";
            public static string ConfirmFail = "No se pudo confirmar su contacto";
        }
    }
}
