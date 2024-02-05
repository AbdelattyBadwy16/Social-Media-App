import Cookies from "universal-cookie";


interface userData {
    token: string,
    username: string
}



export async function GetUserData() {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const username = cookie.get("userName");
    const res = await fetch(`https://localhost:7279/api/Account?UserName=${username}`, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },

    });
    const data = await res.json();
    return data;
}
