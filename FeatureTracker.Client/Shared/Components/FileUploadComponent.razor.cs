using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using MudBlazor;

namespace FeatureTracker.Client.Shared.Components;

public class FileUploadComponentBase : ComponentBase
{
    #region Inject
    [Inject] protected IJSRuntime JSRuntime { get; set; }
    #endregion

    #region Parameters
    [Parameter] public EventCallback<IReadOnlyList<IBrowserFile>> OnFilesChanged { get; set; }
    [Parameter] public int MaxFiles { get; set; } = 5;
    [Parameter] public long MaxFileSize { get; set; } = 10 * 1024 * 1024; // 10MB
    [Parameter] public string[] AllowedExtensions { get; set; } = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".pdf", ".doc", ".docx", ".txt", ".zip", ".rar", ".ico" };
    #endregion

    #region Properties
    protected InputFile? inputFileRef;
    protected List<IBrowserFile> SelectedFiles = new();
    protected string dragClass = string.Empty;
    protected string ErrorMessage = string.Empty;
    protected string _infoDragDropOriginal = "Drag & drop files here, or click to select";
    protected string InfoDragDrop = "Drag & drop files here, or click to select";
    #endregion

    #region Methods
    protected async Task OnInputFileChanged(InputFileChangeEventArgs e)
    {
        await ProcessFiles(e.GetMultipleFiles(MaxFiles));
    }

    protected async Task ProcessFiles(IReadOnlyList<IBrowserFile> files)
    {
        ErrorMessage = string.Empty;
        bool hasChanges = false;

        foreach (var file in files)
        {
            // Check file count limit
            if (SelectedFiles.Count >= MaxFiles)
            {
                ErrorMessage = $"Maximum {MaxFiles} files allowed.";
                break;
            }

            // Check file size
            if (file.Size > MaxFileSize)
            {
                ErrorMessage = $"File '{file.Name}' exceeds the maximum size of {FormatFileSize(MaxFileSize)}.";
                continue;
            }

            // Check file extension
            var extension = Path.GetExtension(file.Name).ToLowerInvariant();
            if (!AllowedExtensions.Contains(extension))
            {
                ErrorMessage = $"File type '{extension}' is not allowed.";
                continue;
            }

            // Check for duplicates
            if (SelectedFiles.Any(f => f.Name == file.Name && f.Size == file.Size))
            {
                ErrorMessage = $"File '{file.Name}' is already selected.";
                continue;
            }

            SelectedFiles.Add(file);
            hasChanges = true;
        }

        if (hasChanges)
        {
            await OnFilesChanged.InvokeAsync(SelectedFiles.AsReadOnly());
            StateHasChanged();
        }
    }

    protected async Task RemoveFile(IBrowserFile file)
    {
        SelectedFiles.Remove(file);
        await OnFilesChanged.InvokeAsync(SelectedFiles.AsReadOnly());
        StateHasChanged();
    }

    protected async Task OpenFileDialog()
    {
        await JSRuntime.InvokeVoidAsync("eval", "document.getElementById('fileInput').click()");
    }

    protected void ClearError()
    {
        ErrorMessage = string.Empty;
        StateHasChanged();
    }

    // Drag and Drop Handlers
    protected void HandleDragEnter()
    {
        dragClass = "drag-over";
        InfoDragDrop = "Drop files here...";
    }

    protected void HandleDragOver()
    {
        // Required for drop to work
    }

    protected void HandleDragLeave()
    {
        dragClass = string.Empty;
        InfoDragDrop = _infoDragDropOriginal;
    }

    protected async Task HandleDrop(DragEventArgs e)
    {
        dragClass = string.Empty;

        // Note: Drag and drop file handling in Blazor WebAssembly has limitations
        // For full drag & drop support, you might need additional JavaScript interop
        await Task.CompletedTask;
    }

    protected string GetFileIcon(IBrowserFile file)
    {
        var extension = Path.GetExtension(file.Name).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" => Icons.Material.Outlined.Image,
            ".pdf" => Icons.Material.Outlined.PictureAsPdf,
            ".doc" or ".docx" => Icons.Material.Outlined.Description,
            ".txt" => Icons.Material.Outlined.TextSnippet,
            ".zip" or ".rar" => Icons.Material.Outlined.Archive,
            ".ico" => Icons.Material.Outlined.Image,
            _ => Icons.Material.Outlined.InsertDriveFile
        };
    }

    protected string GetFileType(IBrowserFile file)
    {
        var extension = Path.GetExtension(file.Name).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" or ".png" or ".gif" or ".bmp" => "image/" + extension.TrimStart('.'),
            ".ico" => "image/x-icon",
            ".pdf" => "application/pdf",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".txt" => "text/plain",
            ".zip" => "application/zip",
            ".rar" => "application/x-rar-compressed",
            _ => "unknown"
        };
    }

    protected string FormatFileSize(long bytes)
    {
        if (bytes < 1024)
            return $"{bytes} B";
        else if (bytes < 1024 * 1024)
            return $"{bytes / 1024.0:F2} KB";
        else if (bytes < 1024 * 1024 * 1024)
            return $"{bytes / (1024.0 * 1024.0):F2} MB";
        else
            return $"{bytes / (1024.0 * 1024.0 * 1024.0):F2} GB";
    }
    #endregion
}
