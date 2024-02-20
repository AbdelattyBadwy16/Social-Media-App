import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.tsx'
import './index.css'
import UserProvider from './Context/UserContext.tsx'
import PostWindowProvider from './Context/PostWindow.tsx'
import UserPostProvider from './Context/UserPostContext.tsx'
import FavPostProvider from './Context/MyFavPost.tsx'

ReactDOM.createRoot(document.getElementById('root')).render(
  <React.StrictMode>
    <PostWindowProvider>
      <UserProvider>
        <UserPostProvider>
          <FavPostProvider>
            <App />
          </FavPostProvider>
        </UserPostProvider>
      </UserProvider>
    </PostWindowProvider>
  </React.StrictMode>,
)
