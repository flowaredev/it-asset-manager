window.downloadFileFromByteArray = (filename, contentType, data) => {
    // Convert base64 string to bytes
    const bytes = new Uint8Array(atob(data).split('').map(c => c.charCodeAt(0)));
    
    // Create blob
    const blob = new Blob([bytes], { type: contentType });
    
    // Create download link
    const url = window.URL.createObjectURL(blob);
    const a = document.createElement('a');
    a.href = url;
    a.download = filename;
    
    // Trigger download
    document.body.appendChild(a);
    a.click();
    
    // Cleanup
    document.body.removeChild(a);
    window.URL.revokeObjectURL(url);
};