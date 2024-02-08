import { Cookie } from "@mui/icons-material";
import axios from "axios"
import { json } from "stream/consumers";
import Cookies from "universal-cookie";


export interface account {

    UserName: string,
    FirstName: string,
    LastName: string,
    Password: string,
    Email: string,
    Gender: string,
    PhoneNumber: string,
    Country: string

}

export async function RegisterNewUser(user: account) {
    const res = await fetch(`https://localhost:7279/api/Account`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({

            userName: user.UserName,
            firstName: user.FirstName,
            lastName: user.LastName,
            password: user.Password,
            email: user.Email,
            gender: user.Gender,
            phoneNumber: user.PhoneNumber,
            country: user.Country , 
            iconImagePath : "profile.jpg"

        }),
    });
    return res;
}



interface accountLogin {

    UserName: string,
    Password: string,

}

export async function LoginUser(user: accountLogin) {
    const res = await fetch(`https://localhost:7279/api/Account/Login`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify({

            username: user.UserName,
            password: user.Password

        }),
    });
    return res;
}


export async function GetAllUsers() {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const res = await fetch(`https://localhost:7279/api/Account/GetAllUser`, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json",
        }
    });
    const data = await res.json();
    return data;
}
