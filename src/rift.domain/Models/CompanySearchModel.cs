using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace rift.domain.Models
{
    public class CompanySearchModel
    {
        public string CNPJ { get; set; }
        public string FantasyName { get; set; }
        public string CompanyName { get; set; }

        public CompanySearchModel()
        {
            CNPJ = string.Empty;
            FantasyName = string.Empty;
            CompanyName = string.Empty;
        }

        public Expression<Func<Company, bool>>[] GetSearchExpressions()
        {
            List<Expression<Func<Company, bool>>> expressions = new List<Expression<Func<Company, bool>>>();

            if (!string.IsNullOrEmpty(CNPJ))
                expressions.Add(x => x.CNPJ.ToLower().Contains(CNPJ.ToLower()));

            if (!string.IsNullOrEmpty(FantasyName))
                expressions.Add(x => x.FantasyName.ToLower().Contains(FantasyName.ToLower()));

            if (!string.IsNullOrEmpty(CompanyName))
                expressions.Add(x => x.CompanyName.ToLower().Contains(CompanyName.ToLower()));

            return expressions.ToArray();
        }
    }
}
