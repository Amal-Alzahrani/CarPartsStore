namespace CarPartsStore.Models
{
    public class Customer
    {
        public int Id { get; set; } // رقم العميل (Primary Key)
        public string FullName { get; set; } // اسم العميل
        public string Email { get; set; } // البريد الإلكتروني
        public string PhoneNumber { get; set; } // رقم الهاتف
        public string Address { get; set; } // العنوان
    }
}
