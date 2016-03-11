using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using React;

namespace ReactJsNodeIssue {
	[TestClass]
	public class UnitTest
	{
		private const string TestJsxCode =
		@"for(var i =0; i<3; ++i) {
			let a = i;
			$('#login-id').click(e => a = a + 1);
		}";

		private static void Initialize()
		{
			Initializer.Initialize(registration => registration.AsSingleton());
			var container = React.AssemblyRegistration.Container;

			container.Register<ICache, NullCache>();
			container.Register<IFileSystem, SimpleFileSystem>();
		}

		[TestInitialize]
		public void SetUp()
		{
			Initialize();
		}

		[TestMethod]
		public void Test()
		{
			IBabel babel = ReactEnvironment.Current.Babel;
			string transformedJs = babel.Transform(TestJsxCode);
			Assert.IsNotNull(transformedJs);
		}
	}
}
