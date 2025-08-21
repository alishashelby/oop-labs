using Itmo.ObjectOrientedProgramming.Lab3.Display;

namespace Itmo.ObjectOrientedProgramming.Lab3.Messenger.Addressee.Entity.Factory;

public class DisplayAddresseeFactory : IAddresseeFactory
{
    private readonly IDisplayDriver _driver;

    public DisplayAddresseeFactory(IDisplayDriver driver)
    {
        _driver = driver;
    }

    public IAddressee Create()
    {
        return new AddresseeDisplay(_driver);
    }
}