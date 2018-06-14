import React from 'react';

const Banner = ({ appName, token }) => {
  if (token) {
    return null;
  }
  return (
    <div className="banner">
      <div className="container">
        <h1 className="logo-font">
          {appName.toLowerCase()}
        </h1>
        <img src="https://preview.ibb.co/b30gKd/e_cicle_logo.png" className="center-block"
         style={{margin: 0 + 'auto'}} alt="e_cicle_logo"  height="80" width="70" border="0"/>
        <p>Saiba como reaproveitar o lixo eletrônico.</p>
      </div>
    </div>
  );
};

export default Banner;
