using CleanArchMvc.Domain.Validation;
using System.Collections.Generic;

namespace CleanArchMvc.Domain.Entities
{
    public sealed class CategoryDto : Entity
    {
        public string Name { get; private set; }
        public CategoryDto(string name)
        {
            ValidateDomain(name);
        }

        public CategoryDto(int id, string name)
        {
            DomainExceptionValidation.When(id < 0, "Invalid Id value.");
            Id = id;
            ValidateDomain(name);
        }

        public void Update(string name)
        {
            ValidateDomain(name);
        }

        public ICollection<Product> Products { get; set; }

        private void ValidateDomain(string name)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(name),
                "Invalid name.Name is required.");

            DomainExceptionValidation.When(name.Length < 3,
              "Invalid name, too short, minimum 3 charecters.");

            Name = name;
        }
    }
}
