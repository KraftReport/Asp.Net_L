namespace TableProjectComponentServiceTestWebAPI.CustomException
{
    public class ResponseModel
    {
        public string Code { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public object Data { get; set; } = string.Empty;
        public int HttpStatus { get; set; } 

        public static ResponseModelBuilder Builder()
        {
            return new ResponseModelBuilder();
        }

        public class ResponseModelBuilder
        {
            private ResponseModel responseModel = new ResponseModel();

            public ResponseModelBuilder Code(string code)
            {
                responseModel.Code = code;
                return this;
            }

            public ResponseModelBuilder Message(string message)
            {
                responseModel.Message = message;
                return this;
            }

            public ResponseModelBuilder Data(object data)
            {
                responseModel.Data = data;
                return this;
            }

            public ResponseModelBuilder HttpStatus(int httpStatus)
            {
                responseModel.HttpStatus = httpStatus;
                return this;
            }

            public ResponseModel Build()
            {
                return responseModel;
            }
        }

    }
}
