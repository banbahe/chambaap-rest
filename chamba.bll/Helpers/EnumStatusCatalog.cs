namespace chambapp.bll.Helpers
{
    public enum EnumStatusCatalog
    {
        Error = -1,
        Null_Empty = 0,
        Ok = 1,
        Email_SentFirstTime = 10, 
        Email_ReplyFirstTime = 11,
        // status for Interviews table
        InterviewProposal = 100,
        // status for Interviews users
        Users_Candidate = 200,
        Users_Recruiter = 201,
    }
}