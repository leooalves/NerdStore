using Microsoft.AspNetCore.Mvc;
using NerdStore.Shared.Mediator;
using System;

namespace NerdStore.Vendas.Api.Controllers
{

    public abstract class PadraoController : ControllerBase
    {        

        protected Guid ClienteId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");

        protected PadraoController()
        {            
    
        }

    }
}
