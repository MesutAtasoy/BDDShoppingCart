using BDDShoppingCart.Specs.Extensions;

namespace BDDShoppingCart.Specs.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void Configure()
        {
            var container = _scenarioContext.ScenarioContainer;

            var configuration = ConfigurationProvider.GetConfiguration();
            
            container.RegisterInstanceAs(configuration);
        }
    }
    
}