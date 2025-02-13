import React from 'react';
import { View } from 'react-native';
import Svg, { Defs, LinearGradient, Path, Stop } from 'react-native-svg';

const EditSvg = ({ isFocused, width = 29, height = 29 }) => {
    return (
        <>
             
<Svg width="24" height="24" viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
<Path d="M18.988 2.01199L21.988 5.01199L19.701 7.29999L16.701 4.29999L18.988 2.01199ZM8 16H11L18.287 8.71299L15.287 5.71299L8 13V16Z" fill="#757575"/>
<Path d="M19 19H8.158C8.132 19 8.105 19.01 8.079 19.01C8.046 19.01 8.013 19.001 7.979 19H5V5H11.847L13.847 3H5C3.897 3 3 3.896 3 5V19C3 20.104 3.897 21 5 21H19C19.5304 21 20.0391 20.7893 20.4142 20.4142C20.7893 20.0391 21 19.5304 21 19V10.332L19 12.332V19Z" fill="#757575"/>
</Svg>

        </>

    );
};

export default EditSvg;
