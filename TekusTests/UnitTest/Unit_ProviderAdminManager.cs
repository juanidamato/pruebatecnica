using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TekusCore.Application.BLL;
using TekusCore.Application.Features.Providers.Querys;
using TekusCore.Application.Interfaces.Repositories;
using TekusCore.Domain.Commons;
using TekusCore.Domain.Entities;

namespace TekusTests.UnitTest
{
   
    public class Unit_ProviderAdminManager
    {
        [Fact]
        public async Task ProviderAdminManager_ProvidingInvalidPaginationValue_ReturnError()
        {
            //arrange
            Mock<ILogger<ProviderAdminManager>> logger = new Mock<ILogger<ProviderAdminManager>>();
            Mock<IProviderRepository> repo = new Mock<IProviderRepository>();
            OperationStatusModel op;
            List<ProviderEntity>? data;
            GetProvidersAdminQuery request = new GetProvidersAdminQuery();
            request.Page = 0;
            request.RecordsPerPage = 1000;
            request.SortDirection = "ASCX";
            request.SortBy = "NAMEX";

            //act
            var sut=new ProviderAdminManager(logger.Object,repo.Object);
            (op, data) = await sut.GetProvidersAdmin(request);

            //assert
            Assert.NotNull(op);
            Assert.Null(data);
            Assert.Equal(OperationResultCodes.BAD_REQUEST, op.code);
        }
    }
}
