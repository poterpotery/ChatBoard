import React from 'react';
import { View } from 'react-native';
import Svg, { Defs, LinearGradient, Path, Stop } from 'react-native-svg';

const ProfileSvg = ({ isFocused, width = 29, height = 29 }) => {
    return (
        <View style={{ paddingRight: 20 }}>
            <Svg width={width ? width : "25"} height={height ? height : "25"} viewBox="0 0 48 48" fill="none" xmlns="http://www.w3.org/2000/svg">
                <Path fill-rule="evenodd" clip-rule="evenodd" d="M24 0.666718C27.0642 0.666703 30.0984 1.27023 32.9293 2.44283C35.7602 3.61543 38.3325 5.33415 40.4992 7.50084C42.6659 9.66754 44.3846 12.2398 45.5573 15.0707C46.7299 17.9017 47.3334 20.9358 47.3334 24C47.3334 36.8867 36.8867 47.3334 24 47.3334C11.1134 47.3334 0.666702 36.8867 0.666702 24C0.666702 11.1135 11.1134 0.666718 24 0.666718ZM26.3334 26.3334H21.6667C15.8901 26.3334 10.9307 29.832 8.79162 34.8262C12.1761 39.5721 17.7267 42.6667 24 42.6667C30.2733 42.6667 35.8239 39.5721 39.2085 34.8258C37.0694 29.832 32.11 26.3334 26.3334 26.3334ZM24 7.66672C20.134 7.66672 17 10.8008 17 14.6667C17 18.5327 20.134 21.6667 24 21.6667C27.866 21.6667 31 18.5327 31 14.6667C31 10.8008 27.8661 7.66672 24 7.66672Z" fill="#757575" />
            </Svg>
        </View>

    );
};

export default ProfileSvg;
