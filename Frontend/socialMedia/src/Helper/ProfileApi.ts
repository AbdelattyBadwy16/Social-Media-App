import Cookies from "universal-cookie";


interface userData {
    token: string,
    username: string
}



export async function GetUserData() {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const id = cookie.get("id");
    const res = await fetch(`https://localhost:7279/api/Account?id=${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },

    });
    const data = await res.json();
    return data;
}


export async function UpdateAbout(about: string) {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const id = cookie.get("id");
    const res = await fetch(`https://localhost:7279/api/Account/about?about=${about}&id=${id}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },

    });
    const data = await res.json();

    return data;
}


export async function UpdateIconImage(image: FormData) {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const id = cookie.get("id");
    
    const res = await fetch(`https://localhost:7279/api/Account/IconImage?id=${id}`, {
        method: "PUT",
        headers: {
            "Authorization": `Bearer ${token}`
        },
        body: image


    });

    return res;
}
