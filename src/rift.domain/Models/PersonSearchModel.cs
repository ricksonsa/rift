using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace rift.domain.Models
{
    public class PersonSearchModel
    {
        public string CPF { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }

        public PersonSearchModel()
        {
            CPF = string.Empty;
            Name = string.Empty;
            Document = string.Empty;
        }

        public Expression<Func<Person, bool>>[] GetSearchExpressions()
        {
            List<Expression<Func<Person, bool>>> expressions = new List<Expression<Func<Person, bool>>>();

            if (!string.IsNullOrEmpty(CPF))
                expressions.Add(x => x.CPF.ToLower().Contains(CPF.ToLower()));

            if (!string.IsNullOrEmpty(Document))
                expressions.Add(x => x.Document.ToLower().Contains(Document.ToLower()));

            if (!string.IsNullOrEmpty(Name))
                expressions.Add(x => x.Name.ToLower().Contains(Name.ToLower()));

            return expressions.ToArray();
        }
    }

}
