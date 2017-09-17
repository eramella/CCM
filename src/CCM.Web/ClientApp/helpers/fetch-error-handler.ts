export function FetchErrorHandler(error) {
    console.error(error);

    if (error.message) {
        return error.message;
    }
    if (!error.ok) {
        let errorStr = '';
        if (error.status) {
            errorStr += 'Status Code: ' + error.status + '; ';
        }
        if (error.statusText) {
            errorStr += 'Message: ' + error.statusText + '; ';
        }
        return errorStr.slice(0, -1);
    }
    return '';
}