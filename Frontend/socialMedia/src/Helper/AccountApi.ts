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
            country: user.Country,
            iconImagePath: "profile.jpg"

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



export async function AddFriends(id: number) {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const userId = cookie.get("id");
    const res = await fetch(`https://localhost:7279/api/Friend/addFriend?id=${userId}&followerId=${id}`, {
        method: "POST",
        headers: {
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json",
        }
    });
    return res;
}



export async function RemoveFriend(id: string) {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const userId = cookie.get("id");
    const res = await fetch(`https://localhost:7279/api/Friend/DeleteFriend?userId=${userId}&id=${id}`, {
        method: "Delete",
        headers: {
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json",
        }
    });

    return res;
}



export async function GetFriends() {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const userId = cookie.get("id");
    const res = await fetch(`https://localhost:7279/api/Friend/GetUserFollowing?id=${userId}`, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json",
        }
    });
    const data = await res.json();
    return data;
}


export async function GetFollower() {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const userId = cookie.get("id");
    const res = await fetch(`https://localhost:7279/api/Friend/GetUserFollower?id=${userId}`, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json",
        }
    });
    const data = await res.json();
    return data;
}

export async function CheckFriend(id: string) {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const userId = cookie.get("id");
    const res = await fetch(`https://localhost:7279/api/Friend/CheckFriend?userId=${userId}&id=${id}`, {
        method: "GET",
        headers: {
            "Authorization": `Bearer ${token}`,
            "Content-Type": "application/json",
        }
    });
    const data = await res.json();
    return data;
}



export interface UpdateAccount {
    id: string,
    FirstName: string,
    LastName: string,
    nickName: string,
    Password: string,
    oldPassword: string,
    BirhDate: string,
    Country: string,
    JopTitle: string,
    PhoneNumber: string

}


export async function UpdateAccount(account: UpdateAccount) {
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const userId = cookie.get("id");
    try {
        const res = await fetch(`https://localhost:7279/api/Account/updateAccount`, {
            method: "PUT",
            headers: {
                "Authorization": `Bearer ${token}`,
                "Content-Type": "application/json",
            },
            body: JSON.stringify({

                id: userId,
                firstName: account.FirstName,
                lastName: account.LastName,
                nickName: account.nickName,
                password: account.Password,
                oldPassword: account.oldPassword,
                birthDate: account.BirhDate,
                phoneNumber: account.PhoneNumber,
                country: account.Country,
                jopTitle: account.JopTitle

            }),
        });
        
        if(res.ok){
            return res;
        }else return await res.text();
    } catch {
        
        return "Wronge Old Password.";
    }
   return ;
}