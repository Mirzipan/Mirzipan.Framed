using Mirzipan.Bibliotheca.Disposables;

namespace Mirzipan.Framed
{
    public interface IContainDisposer
    {
        CompositeDisposable Disposer { get; set; }
    }
}