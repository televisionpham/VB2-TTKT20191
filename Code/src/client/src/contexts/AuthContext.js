import React, {createContext, useReducer, useEffect} from 'react';
import { authReducer } from '../store/reducers/authReducer';

export const AuthContext = createContext();

const AuthContextProvider = (props) => {
    const initState = { 
        userId: null,        
        email: '',        
    }
    const [auth, dispatch] = useReducer(authReducer, initState, () => {
        const localData = localStorage.getItem('authUser');
        return localData ? JSON.parse(localData) : initState;
    });

    useEffect(() => {
        localStorage.setItem('authUser', JSON.stringify(auth));
    }, [auth]);

    return ( 
        <AuthContext.Provider value={{auth, dispatch}}>
            { props.children }
        </AuthContext.Provider>
     );
}
 
export default AuthContextProvider;