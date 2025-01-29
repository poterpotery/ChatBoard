import React from 'react';
import { View } from 'react-native';
import Svg, { Defs, LinearGradient, Path, Stop } from 'react-native-svg';

const DeleteTaskSvg = ({ isFocused, width = 29, height = 29 }) => {
    return (
        <>

            <Svg width="24" height="20" viewBox="0 0 14 18" fill="none" xmlns="http://www.w3.org/2000/svg">
                <Path d="M1 16C1 17.1 1.9 18 3 18H11C12.1 18 13 17.1 13 16V4H1V16ZM14 1H10.5L9.5 0H4.5L3.5 1H0V3H14V1Z" fill="red" />
            </Svg>

        </>

    );
};

export default DeleteTaskSvg;
