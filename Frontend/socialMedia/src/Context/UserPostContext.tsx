import React, { createContext, useState } from 'react'

export const UserPost = createContext({});

export default function UserPostProvider({ children } : any) {

    const [post, setPost] = useState([]);

    return (
        <UserPost.Provider value={{ post, setPost }}>{children}</UserPost.Provider>
    )
}
