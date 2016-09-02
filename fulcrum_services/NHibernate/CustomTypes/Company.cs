using System.Collections.Generic;
using System.Reflection;

namespace fulcrum_services.NHibernate.CustomTypes
{
    public class Company : ICustomType
    {
        public readonly static Company MINE = new Company("MNE", "jordanalphonso.net");
        public readonly static Company NA = new Company("NA", "N/A");
        public readonly static Company LUMA = new Company("LUMA", "LUMA");

        public string _code { get; private set; }
        public string _label { get; private set; }

        public Company(string code, string label)
        {
            _code = code;
            _label = label;
        }

        public static Company getCompany(string codeOrLabel)
        {
            var fields = typeof(Company).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var f in fields)
            {
                Company company = (Company)f.GetValue(null);
                if (company._code.ToLower().Equals(codeOrLabel) ||
                    company._label.ToLower().Equals(codeOrLabel))
                {
                    return company;
                }
            }  
            return null;
        }

        public static IList<Company> getAllCompanies()
        {
            IList<Company> companies = new List<Company>();
            var fields = typeof(Company).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (var f in fields)
            {
                Company company = (Company)f.GetValue(null);
                companies.Add(company);
            }
            return companies;
        }

        public ICustomType getType(string codeOrLabel)
        {
            return getCompany(codeOrLabel);
        }

        public object getCode()
        {
            return _code;
        }
    }
}
