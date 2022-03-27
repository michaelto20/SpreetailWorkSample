using Moq;
using Spreetail.Core.Services.DictionaryService;
using Spreetail.Infrastructure.Services.MembersCommandService;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Spreetail.Infrastructure.UnitTest.Services
{
    public class MembersCommandSerivceUnitTest
    {
        private readonly Mock<IDictionaryService<string, string>> _mockDictionaryService;

        public MembersCommandSerivceUnitTest()
        {
            _mockDictionaryService = new Mock<IDictionaryService<string, string>>();
        }

        private MembersCommandService<string, string> GetService()
        {
            return new MembersCommandService<string, string>(_mockDictionaryService.Object);
        }

    }
}
