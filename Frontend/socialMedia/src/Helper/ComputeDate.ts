

export function ComputeDate(date : string) {
    if(date == undefined)return 0;
    const PostDate = new Date(date);
    const minuts = new Date().getMinutes();
    const hours = new Date().getHours();
    const day = new Date().getDay();
    const month = new Date().getDay();
    const year = new Date().getFullYear();
    
    if(year != PostDate.getFullYear()) return "year";
    if(month != PostDate.getMonth()) return "month";
    if(day != PostDate.getDay()) return "day";
    if(hours != PostDate.getHours())return "hours";
    return "minuts";
    
}