import Cookies from "universal-cookie";



export async function GetUserPhotosTop3() {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const id = cookie.get("id");
    try {
        const res = await fetch(`https://localhost:7279/api/Photo?id=${id}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
        });

        var data = res.json();
    } catch {
        throw new Error("Can't get Images.");
    }
    return data;
}


export async function GetUserPhotos() {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const id = cookie.get("id");
    try {
        const res = await fetch(`https://localhost:7279/api/Photo/AllPhotos?id=${id}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
        });

        var data = res.json();
    } catch {
        throw new Error("Can't get Images.");
    }
    return data;
}