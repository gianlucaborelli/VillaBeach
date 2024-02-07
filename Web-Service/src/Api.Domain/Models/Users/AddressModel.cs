namespace Api.Domain.Models
{
    public class AddressModel : OwnerBaseModel
    {

        private string _postalCode = string.Empty;
        public string PostalCode
        {
            get { return _postalCode; }
            set { _postalCode = value; }
        }

        private string _street = string.Empty;
        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }

        private string _number = string.Empty;
        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }

        private string _district = string.Empty;
        public string District
        {
            get { return _district; }
            set { _district = value; }
        }

        private string _city = string.Empty;
        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        private string _state = string.Empty;
        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        private string _description = string.Empty;
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}