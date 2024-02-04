import axios from "axios"
import { json } from "stream/consumers";


interface account {

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
            country: user.Country

        }),
    });
    return res;
}



interface accountLogin {

    UserName: string,
    Password: string,

}

export async function LoginUser(user : accountLogin) {
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