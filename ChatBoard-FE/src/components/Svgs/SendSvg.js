import React from 'react';
import { View } from 'react-native';
import Svg, { Defs, LinearGradient, Path, Stop } from 'react-native-svg';

const SendSvg = ({ isFocused, width = 29, height = 29 }) => {
    return (
        <>
            <Svg width="20" height="20" viewBox="0 0 27 27" fill="none" xmlns="http://www.w3.org/2000/svg">
                <Path d="M26 1L1 10.3015L14.1579 12.8476L17.4508 26L26 1Z" stroke="white" stroke-width="2" stroke-linejoin="round" />
                <Path d="M14.1633 12.8476L17.8849 9.12605" stroke="white" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" />
            </Svg>
        </>

    );
};

export default SendSvg;
