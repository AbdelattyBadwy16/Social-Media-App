import React, { createContext, useState } from 'react'

export const FavPost = createContext({});

export default function FavPostProvider({ children }) {

    const [post, setPost] = useState([]);

    return (
        <FavPost.Provider value={{ post, setPost }}>{children}</FavPost.Provider>
    )
}
