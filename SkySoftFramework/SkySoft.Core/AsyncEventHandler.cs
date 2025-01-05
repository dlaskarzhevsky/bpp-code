namespace SkySoft.Core
{
    /// <summary>
    /// Provides AsyncEventHandler delegate functionality
    /// </summary>
    /// <typeparam name="TEventArgs">Event arguments type</typeparam>
    /// <param name="sender">Event source</param>
    /// <param name="e">Event arguments</param>
    /// <returns>Event result</returns>
    public delegate Task AsyncEventHandler<TEventArgs>(object? sender, TEventArgs e);
}
