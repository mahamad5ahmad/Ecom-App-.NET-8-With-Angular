namespace Ecom.API.Helper
{
    public class ResponseAPI
    {

        public ResponseAPI(int statuscode , string message=null )
        {
            this.statuscode = statuscode;
            this.message = message?? getmessagefromstatuscode(this.statuscode) ;


        }
        private string getmessagefromstatuscode(int statuscode)
        {
            return statuscode switch
            {
                200 => "seccess",
                201 => "created",
                204 => "no content",
                400 => "bad request",
                401 => "unauthorized",
                403 => "forbidden",
                404 => "not found",
                500 => "internal server error",
                _ => "unknown error",
            };
            

        }
        public int statuscode { get; set; }
        public string message { get; set; }

    }
}
