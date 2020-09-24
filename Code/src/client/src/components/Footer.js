import React from 'react';

const Footer = () => {
    return (
        <footer style={{ margin: '8px', textAlign: 'center', padding: '8px'}}>
            <span>&copy; {new Date().getFullYear()} VYT. All rights reserved</span>
        </footer>
    );
}

export default Footer;