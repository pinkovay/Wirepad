using System;

namespace Wirepad.Cli.Common.Contracts;

public interface IOutputFormatter
{
    void DisplayContent(string content);
    void DisplayWatchUpdate(string content);
    void WriteMessage(string message);
    void WriteSuccess(string message);
    void WriteError(string message);
    void WriteWarning(string message);
}
