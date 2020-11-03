import { SIGNIN_SUCCESS, SIGNOUT, SIGNUP_SUCCESS } from "../../constants";

const initState = {    
    userId: null,
    email: '',    
}

export const authReducer = (state = initState, action) => {

    switch (action.type) {        
        case SIGNIN_SUCCESS:               
            return {
                ...state,
                userId: action.cred.userId,
                email: action.cred.email,                
            }        
        case SIGNOUT: {            
            return {
                ...state,                
                userId: null,
                email: '',                
            }
        }
        case SIGNUP_SUCCESS:            
            return {
                ...state,
                userId: action.id,                
            };

        default:
            return state;
    }
}