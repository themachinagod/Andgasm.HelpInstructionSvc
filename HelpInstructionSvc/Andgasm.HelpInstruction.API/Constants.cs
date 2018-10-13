namespace Andgasm.HelpInstruction.API
{
    public static class Constants
    {
        public const string InvalidIdProvided = "The specified id '{0}' was not valid";
        public const string IdNotFound = "The specified id '{0}' was not found in the data store";

        public const string InvalidLookupProvided = "The specified lookup key '{0}' was not valid";
        public const string LookupNotFound = "The specified lookup key '{0}' was not found in the data store";

        public const string InvalidRequestPayload = "No payload data was recieved to action the request";

        public const string PrimaryKeyViolation = "Cannot store to data store: Primary key '{0}' already exists!";
        public const string LookupKeyViolation = "Cannot store to data store: Lookup key '{0}' already exists!";

        public const string InvalidPagingTakeOptions = "Request must specify number of records to retrieve in the take option, must be greater than 0!";

        public const string InvalidPagingSkipOptions = "Request must specify number of records to skip in the skip option, must be 0 or greater!";

        public const int DefaultReportPageSize = 10;
    }
}
