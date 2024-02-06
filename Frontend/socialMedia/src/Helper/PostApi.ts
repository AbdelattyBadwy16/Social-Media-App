import Cookies from "universal-cookie";
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';


interface Post {
    content: string,
    status: string,
    userId: string,
    groupId: 0 ,
    file : FormData
}


export async function CreateNewPost(post: Post ) {
    //handel close post
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    try {
        const res = await fetch(`https://localhost:7279/api/Post`, {
            method: "POST",
            headers: {
                "Authorization": `Bearer ${token}`
            },
            body: JSON.stringify({
                content: post.content,
                status: post.status,
                userId: post.userId,
                file : post.file,
                groupId: null
            })

        });
        return res;
    } catch {
        toast("Post Creating Failled!");
    }
    return;
}


export async function GetPost(id: number) {
    //handel close post
    const cookie = new Cookies();
    const token = cookie.get("bearer");

    try {
        const res = await fetch(`https://localhost:7279/api/Post/getPost?id=${id}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
        });
        const data = await res.json();
        return data;
    } catch {
        toast("Nertwork Problem !! ");
    }
    return;
}



export async function GetALLPosts() {
    //handel close post
    const cookie = new Cookies();
    const token = cookie.get("bearer");

    try {
        const res = await fetch(`https://localhost:7279/api/Post`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
        });
        const data = await res.json();
        return data;
    } catch {
        toast("Nertwork Problem !! ");
    }
    return;
}



export async function GetUserPosts() {

    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const id = cookie.get("id");
    try {
        const res = await fetch(`https://localhost:7279/api/Post/userPost?userId=${id}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
        });
        const data = await res.json();
        return data;
    } catch {
        toast("Nertwork Problem !! ");
    }
    return;
}



export async function DeletePost(id: number) {

    const cookie = new Cookies();
    const token = cookie.get("bearer");
    try {
        const res = await fetch(`https://localhost:7279/api/Post?id=${id}`, {
            method: "DELETE",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
        });
        return res;
    } catch {
        toast("Nertwork Problem !! ");
    }
    return;
}


export async function UpdateReacts(id: number, type: string ) {

    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const userId = cookie.get("id");
    try {
        const res = await fetch(`https://localhost:7279/api/Post?type=${type}&id=${id}&userId=${userId}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
        });
        return res;
    } catch {
        toast("Nertwork Problem !! ");
    }
    return;
}


export async function UpdatePost( content: string , status : string ) {

    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const Id = cookie.get("PostId");
    try {
        const res = await fetch(`https://localhost:7279/api/Post/EditPost?content=${content}&status=${status}&id=${Id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
        });
        return res;
    } catch {
        toast("Nertwork Problem !! ");
    }
    return;
}




export async function CheckPostReact(id: number) {
    if(id == undefined)return;
    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const userId = cookie.get("id");
    try {
        const res = await fetch(`https://localhost:7279/api/Post/CheckReact?userId=${userId}&id=${id}`, {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
        });
        const data = res.text();
        return data;
    } catch {
        toast("Nertwork Problem !! ");
    }
    return;
}


export async function RemovePostReact(id: number) {

    const cookie = new Cookies();
    const token = cookie.get("bearer");
    const userId = cookie.get("id");
    try {
        const res = await fetch(`https://localhost:7279/api/Post/RemoveReact?userId=${userId}&id=${id}`, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
                "Authorization": `Bearer ${token}`
            },
        });
        return res;
    } catch {
        toast("Nertwork Problem !! ");
    }
    return;
}