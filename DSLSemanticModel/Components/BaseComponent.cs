using System;
using System.Collections.Generic;
using System.Text;

namespace DSLSemanticModel.Components
{
    public interface IComponent
    {
        public Guid Id { get; }
        public string Generate();
    }

    public class BaseComponent : IComponent
    {
        public BaseComponent()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }

        public virtual string Generate()
        {
            return "This is UI Generated Component";
        }
    }
}
