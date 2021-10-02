namespace Playground.PaymentEngine.Rules.Store.File;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using RulesEngine.Models;

public class FileRuleStore: RuleStore {
    private readonly IFileProvider _fileProvider;

    public FileRuleStore(IFileProvider fileProvider) =>
        _fileProvider = fileProvider;
    
    public async Task<IEnumerable<WorkflowRules>> GetRulesAsync(string name, CancellationToken cancellationToken) {
        var json = await GetRulesJsonAsync(name, cancellationToken);
        return JsonConvert.DeserializeObject<List<WorkflowRules>>(json)!;
    }
    
    private async Task<string> GetRulesJsonAsync(string name, CancellationToken cancellationToken) {
        var dir = _fileProvider.GetDirectoryContents("Rules");
        var file = dir.FirstOrDefault(f => f.Name.Equals($"{name}.rule.json"));

        if (file == null)
            return string.Empty;
        
        var contents = new StreamReader(file.CreateReadStream());
        return await contents.ReadToEndAsync();
    }
}