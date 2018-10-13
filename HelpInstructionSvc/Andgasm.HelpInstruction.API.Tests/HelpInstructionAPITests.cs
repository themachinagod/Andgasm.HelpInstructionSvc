using Andgasm.API.Core;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using SE.DynamicHelp.API.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Andgasm.HelpInstruction.API.Tests
{ 
    public class HelpInstructionAPITests
    {
        #region Fields
        HelpInstructionController _controller;
        #endregion

        #region Constructors
        public HelpInstructionAPITests()
        {
        }
        #endregion

        #region GetAll Tests
        [Fact]
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
            Setup();
            var r = _controller.GetAll();
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Single(valresult);
        }
        #endregion

        #region GetById Tests
        [Fact]
        public void GetById_WhenCalledWithValidId_ReturnsOkResult()
        {
            Setup();
            var r = _controller.GetById(1);
            var objectResult = Assert.IsType<OkObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<HelpInstructionResource>(objectResult.Value);
            Assert.NotNull(valresult);
        }

        [Fact]
        public void GetById_WhenCalledWithInvalidId_ReturnsBadRequestResult()
        {
            Setup();
            var r = _controller.GetById(0);
            var objectResult = Assert.IsType<BadRequestObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(string.Format(Constants.InvalidIdProvided, 0), valresult);
        }

        [Fact]
        public void GetById_WhenCalledWithNotExistingId_ReturnsNotFoundResult()
        {
            Setup();
            var r = _controller.GetById(2);
            var objectResult = Assert.IsType<NotFoundObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(string.Format(Constants.IdNotFound, 2), valresult);
        }
        #endregion

        #region GetByLookupKey Tests
        [Fact]
        public void GetByLookupKey_WhenCalledWithValidLookup_ReturnsOkResult()
        {
            Setup();
            var r = _controller.GetByLookupKey("testkey1");
            var objectResult = Assert.IsType<OkObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<HelpInstructionResource>(objectResult.Value);
            Assert.NotNull(valresult);
        }

        [Fact]
        public void GetByLookupKey_WhenCalledWithInvalidLookup_ReturnsBadRequestResult()
        {
            Setup();
            var r = _controller.GetByLookupKey("");
            var objectResult = Assert.IsType<BadRequestObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(string.Format(Constants.InvalidLookupProvided, ""), valresult);
        }

        [Fact]
        public void GetByLookupKey_WhenCalledWithNotExistingLookup_ReturnsNotFoundResult()
        {
            Setup();
            var r = _controller.GetByLookupKey("idontexist");
            var objectResult = Assert.IsType<NotFoundObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(string.Format(Constants.LookupNotFound, "idontexist"), valresult);
        }
        #endregion

        #region ReportByOptions Tests
        [Fact]
        public void Report_WhenCalledWithNoOptions_ReturnsOkResultWithDefaultPaging()
        {
            Setup(20);
            var r = _controller.ReportByOptions(null);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(Constants.DefaultReportPageSize, valresult.Count);
        }

        [Fact]
        public void Report_WhenCalledWithValidPageOptions_ReturnsOkResultWithSpecifiedPaging()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 5 };
            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(5, valresult.Count);
        }

        [Fact]
        public void Report_WhenCalledWithInvalidPageTakeOptions_ReturnsBadRequestResult()
        {
            Setup();
            var opts = new ReportOptions() { skip = 0, take = 0 };
            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<BadRequestObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(Constants.InvalidPagingTakeOptions, valresult);
        }

        [Fact]
        public void Report_WhenCalledWithInvalidPageSkipOptions_ReturnsBadRequestResult()
        {
            Setup();
            var opts = new ReportOptions() { skip = -1, take = 5 };
            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<BadRequestObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(Constants.InvalidPagingSkipOptions, valresult);
        }

        [Fact]
        public void Report_WhenCalledWithValidSingleEqualsFilterOptions_ReturnsOkResultWithSpecifiedFilter()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 10 };
            opts.filter = new FilterOptions[1];
            opts.filter[0] = new FilterOptions() { field = "lookupKey", @operator = FilterOperator.eq, value = "testkey15" };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Single(valresult);
        }

        [Fact]
        public void Report_WhenCalledWithValidSingleNotEqualsFilterOptions_ReturnsOkResultWithSpecifiedFilter()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.filter = new FilterOptions[1];
            opts.filter[0] = new FilterOptions() { field = "lookupKey", @operator = FilterOperator.neq, value = "testkey15" };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(19, valresult.Count);
        }

        [Fact]
        public void Report_WhenCalledWithValidSingleContainsFilterOptions_ReturnsOkResultWithSpecifiedFilter()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.filter = new FilterOptions[1];
            opts.filter[0] = new FilterOptions() { field = "lookupKey", @operator = FilterOperator.contains, value = "testkey1" };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(11, valresult.Count);
        }

        [Fact]
        public void Report_WhenCalledWithValidSingleStartsWithFilterOptions_ReturnsOkResultWithSpecifiedFilter()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.filter = new FilterOptions[1];
            opts.filter[0] = new FilterOptions() { field = "lookupKey", @operator = FilterOperator.startswith, value = "testkey1" };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(11, valresult.Count);
        }

        [Fact]
        public void Report_WhenCalledWithValidSingleEndsWithFilterOptions_ReturnsOkResultWithSpecifiedFilter()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.filter = new FilterOptions[1];
            opts.filter[0] = new FilterOptions() { field = "lookupKey", @operator = FilterOperator.endswith, value = "key10" };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Single(valresult);
        }

        [Fact]
        public void Report_WhenCalledWithValidSingleGreaterThanFilterOptions_ReturnsOkResultWithSpecifiedFilter()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.filter = new FilterOptions[1];
            opts.filter[0] = new FilterOptions() { field = "internalId", @operator = FilterOperator.gt, value = 10 };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(10, valresult.Count);
        }

        [Fact]
        public void Report_WhenCalledWithValidSingleGreaterThanEqualsFilterOptions_ReturnsOkResultWithSpecifiedFilter()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.filter = new FilterOptions[1];
            opts.filter[0] = new FilterOptions() { field = "internalId", @operator = FilterOperator.gte, value = 10 };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(11, valresult.Count);
        }

        [Fact]
        public void Report_WhenCalledWithValidSingleLessThanFilterOptions_ReturnsOkResultWithSpecifiedFilter()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.filter = new FilterOptions[1];
            opts.filter[0] = new FilterOptions() { field = "internalId", @operator = FilterOperator.lt, value = 10 };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(9, valresult.Count);
        }

        [Fact]
        public void Report_WhenCalledWithValidSingleLessThanEqualsFilterOptions_ReturnsOkResultWithSpecifiedFilter()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.filter = new FilterOptions[1];
            opts.filter[0] = new FilterOptions() { field = "internalId", @operator = FilterOperator.lte, value = 10 };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(10, valresult.Count);
        }

        [Fact]
        public void Report_WhenCalledWithInvalidColumnFilterOptions_ReturnsOkResultWithIgnoredInvalidFilter()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.filter = new FilterOptions[1];
            opts.filter[0] = new FilterOptions() { field = "badcol", @operator = FilterOperator.eq, value = "avalue" };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(20, valresult.Count);
        }

        [Fact]
        public void Report_WhenCalledWithInvalidDataTypeFilterOptions_ReturnsOkResultWithIgnoredInvalidFilter()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.filter = new FilterOptions[1];
            opts.filter[0] = new FilterOptions() { field = "internalId", @operator = FilterOperator.eq, value = "abadvalue" };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(20, valresult.Count);
        }

        // TODO: valid sorts
        [Fact]
        public void Report_WhenCalledWithValidSingleDescSortOptions_ReturnsOkResultWithSpecifiedSort()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.sort = new SortOptions[1];
            opts.sort[0] = new SortOptions() { field = "internalId", dir = SortDirection.desc };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(20, valresult.Count);
            Assert.Equal(20, valresult.First().InternalId);
            Assert.Equal(1, valresult.Last().InternalId);
        }

        [Fact]
        public void Report_WhenCalledWithValidSingleAscSortOptions_ReturnsOkResultWithSpecifiedSort()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.sort = new SortOptions[1];
            opts.sort[0] = new SortOptions() { field = "internalId", dir = SortDirection.asc };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(20, valresult.Count);
            Assert.Equal(1, valresult.First().InternalId);
            Assert.Equal(20, valresult.Last().InternalId);
        }

        [Fact]
        public void Report_WhenCalledWithInvalidColumnSortOptions_ReturnsOkResultWithIgnoredSort()
        {
            Setup(20);
            var opts = new ReportOptions() { skip = 0, take = 20 };
            opts.sort = new SortOptions[1];
            opts.sort[0] = new SortOptions() { field = "badcol", dir = SortDirection.asc };

            var r = _controller.ReportByOptions(opts);
            var objectResult = Assert.IsType<OkObjectResult>(r);
            var valresult = Assert.IsAssignableFrom<List<HelpInstructionResource>>(objectResult.Value);
            Assert.Equal(20, valresult.Count);
            Assert.Equal(1, valresult.First().InternalId);
            Assert.Equal(20, valresult.Last().InternalId);
        }
        #endregion

        #region Create Tests
        [Fact]
        public void Create_WhenCalledWithValidData_ReturnsOkResult()
        {
            Setup();
            var hir = new HelpInstructionResource()
            {
                InternalId = 100,
                ExternalId = "createtest",
                HostKey = "createhostkey",
                LookupKey = "createlookupkey",
                TooltipText = "create tooltip text"
            };
            var r = _controller.Create(hir);
            var objectResult = Assert.IsType<OkObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<HelpInstructionResource>(objectResult.Value);
            Assert.Equal(100, valresult.InternalId);
            Assert.Equal("createtest", valresult.ExternalId);
            Assert.Equal("createhostkey", valresult.HostKey);
            Assert.Equal("createlookupkey", valresult.LookupKey);
            Assert.Equal("create tooltip text", valresult.TooltipText);
        }

        [Fact]
        public void Create_WhenCalledWithNoData_ReturnsBadRequestResult()
        {
            Setup();
            var r = _controller.Create(null);
            var objectResult = Assert.IsType<BadRequestObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(Constants.InvalidRequestPayload, valresult);
        }

        [Fact]
        public void Create_WhenCalledWithDupeIdData_ReturnsConflictResult()
        {
            Setup();
            var hir = new HelpInstructionResource()
            {
                InternalId = 1,
                ExternalId = "createtest",
                HostKey = "createhostkey",
                LookupKey = "createlookupkey",
                TooltipText = "create tooltip text"
            };
            var r = _controller.Create(hir);
            var objectResult = Assert.IsType<ConflictObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(string.Format(Constants.PrimaryKeyViolation, 1), valresult);
        }

        [Fact]
        public void Create_WhenCalledWithDupeLookupData_ReturnsConflictResult()
        {
            Setup();
            var hir = new HelpInstructionResource()
            {
                InternalId = 101,
                ExternalId = "badcreatetest",
                HostKey = "badcreatehostkey",
                LookupKey = "testkey1",
                TooltipText = "create tooltip text which should never find the db"
            };
            var r = _controller.Create(hir);
            var objectResult = Assert.IsType<ConflictObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(string.Format(Constants.LookupKeyViolation, "testkey1"), valresult);
        }
        #endregion

        #region UpdateById Tests
        [Fact]
        public void UpdateById_WhenCalledWithValidData_ReturnsOkResult()
        {
            Setup();
            var hir = new HelpInstructionResource()
            {
                InternalId = 1,
                ExternalId = "testupd",
                HostKey = "testhostupd",
                LookupKey = "testkeyupd",
                TooltipText = "test tooltip text updated"
            };
            var r = _controller.UpdateById(1, hir);
            var objectResult = Assert.IsType<OkObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<HelpInstructionResource>(objectResult.Value);
            Assert.Equal(1, valresult.InternalId);
            Assert.Equal("testupd", valresult.ExternalId);
            Assert.Equal("testhostupd", valresult.HostKey);
            Assert.Equal("testkeyupd", valresult.LookupKey);
            Assert.Equal("test tooltip text updated", valresult.TooltipText);
        }

        [Fact]
        public void UpdateById_WhenCalledWithNoData_ReturnsBadRequestResult()
        {
            Setup();
            var r = _controller.UpdateById(1, null);
            var objectResult = Assert.IsType<BadRequestObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(Constants.InvalidRequestPayload, valresult);
        }

        [Fact]
        public void UpdateById_WhenCalledWithNoId_ReturnsBadRequestResult()
        {
            Setup();
            var hir = new HelpInstructionResource()
            {
                ExternalId = "test",
                HostKey = "testhost",
                LookupKey = "testkey",
                TooltipText = "test tooltip text"
            };
            var r = _controller.UpdateById(0, hir);
            var objectResult = Assert.IsType<BadRequestObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(string.Format(Constants.InvalidIdProvided, 0), valresult);
        }

        [Fact]
        public void UpdateById_WhenCalledWithNotExistingId_ReturnsNotFoundResult()
        {
            Setup();
            var hir = new HelpInstructionResource()
            {
                InternalId = 1000,
                ExternalId = "updfailtest",
                HostKey = "updfailtest",
                LookupKey = "updfailtest",
                TooltipText = "updfailtest"
            };
            var r = _controller.UpdateById(1000, hir);
            var objectResult = Assert.IsType<NotFoundObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(string.Format(Constants.IdNotFound, 1000), valresult);
        }
        #endregion

        #region DeleteById Tests
        [Fact]
        public void DeleteById_WhenCalledWithValidData_ReturnsOkResult()
        {
            Setup();
            var r = _controller.DeleteById(1);
            var objectResult = Assert.IsType<OkResult>(r.Result);
        }

        [Fact]
        public void DeleteById_WhenCalledWithNoId_ReturnsBadRequestResult()
        {
            Setup();
            var r = _controller.DeleteById(0);
            var objectResult = Assert.IsType<BadRequestObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(string.Format(Constants.InvalidIdProvided, 0), valresult);
        }

        [Fact]
        public void DeleteById_WhenCalledWithNotExistingId_ReturnsNotFoundResult()
        {
            Setup();
            var r = _controller.DeleteById(2000);
            var objectResult = Assert.IsType<NotFoundObjectResult>(r.Result);
            var valresult = Assert.IsAssignableFrom<string>(objectResult.Value);
            Assert.Equal(string.Format(Constants.IdNotFound, 2000), valresult);
        }
        #endregion

        #region Options Tests
        [Fact]
        public void Options_WhenCalledWithNoId_ReturnsOkResult()
        {
            Setup();
            var r = _controller.Options();
            var objectResult = Assert.IsType<OkResult>(r);
        }

        [Fact]
        public void Options_WhenCalledWithId_ReturnsOkResult()
        {
            Setup();
            var r = _controller.Options(1);
            var objectResult = Assert.IsType<OkResult>(r);
        }
        #endregion

        #region Setup
        private void Setup(int datarowstoadd = 1)
        {
            var context = MockAPIDbContext(datarowstoadd);
            var mapper = MockMapper();
            _controller = new HelpInstructionController(context, mapper, 
                                                        new DynamicExpressionService(new NullLogger<DynamicExpressionService>()), 
                                                        new NullLogger<HelpInstructionController>());
        }

        private APIDBContext MockAPIDbContext(int datarowstoadd = 1)
        {
            var options = new DbContextOptionsBuilder<APIDBContext>()
                      .UseInMemoryDatabase(Guid.NewGuid().ToString())
                      .Options;
            var context = new APIDBContext(options);

            for (int i = 1; i <= datarowstoadd; i++)
            {
                var testhi = new HelpInstruction()
                {
                    Id = i,
                    ExternalId = "test" + i,
                    HostKey = "testhost" + i,
                    LookupKey = "testkey" + i,
                    TooltipText = "test tooltip text" + i
                };
                context.HelpInstructions.Add(testhi);
            }
            context.SaveChanges();
            return context;
        }
        private IMapper MockMapper()
        {
            var profile = new MappingProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            return new Mapper(configuration);
        }
        #endregion
    }
}
