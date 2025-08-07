namespace AiCompany.Dtos
{
    public class CompanyRegisterDto
    {
        public string ArabicName { get; set; }
        public string EnglishName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Website { get; set; }

        public IFormFile? Logo { get; set; }
    }

}
