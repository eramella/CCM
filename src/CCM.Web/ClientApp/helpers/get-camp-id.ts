export function GetCampId() {
    let path = location.pathname;
    let idString = path.match(/(?:\/camp\/)(\d*)/)[1];
    let id = parseInt(idString);
    if (isNaN(id)) return 0;
    return id;
}